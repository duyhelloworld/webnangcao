using System.Net;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Exceptions;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services.Impls;

public class TrackService : ITrackService
{
    private readonly ApplicationContext _context;

    public TrackService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackResponseModel>> GetAll()
    {
        var result = from track in _context.Tracks
            join userTrackAction in _context.UserTrackActions
                on track.Id equals userTrackAction.TrackId
            join user in _context.Users
                on userTrackAction.UserId equals user.Id
            join comment in _context.Comments
                on track.Id equals comment.TrackId
        select new TrackResponseModel()
        {
            Id = track.Id,
            Author = user.UserName!,
            TrackName = track.Name,
            ArtWork = track.ArtWork,
            UploadAt = userTrackAction.CreatedAt,
            ListenCount = track.UserActions.Count(ua => ua.ActionType == EUserTrackActionType.PLAY),
            LikeCount = track.UserActions.Count(ua => ua.ActionType == EUserTrackActionType.LIKE),
            CommentCount = track.Comments.Count,
        };
        return await Task.FromResult(result);
    }

    public async Task<TrackResponseModel?> GetById(int id)
    {
        var track = await _context.Tracks
            .Include(t => t.UserActions)
            .Include(t => t.Comments)
            .Include(t => t.Categories)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không tìm thấy bài hát", "Hãy thử lại");
        return new TrackResponseModel()
        {
            Id = track.Id,
            Author = track.UserActions
                .FirstOrDefault(ua => ua.ActionType == EUserTrackActionType.UPLOAD)!
                .User.UserName ?? "Unknown",
            TrackName = track.Name,
            ListenCount = track.UserActions.Count(ua => ua.ActionType == EUserTrackActionType.PLAY),
            LikeCount = track.UserActions.Count(ua => ua.ActionType == EUserTrackActionType.LIKE),
            CommentCount = track.Comments.Count,
            ArtWork = track.ArtWork,
            UploadAt = track.UserActions.FirstOrDefault(ua => ua.ActionType == EUserTrackActionType.UPLOAD)!.CreatedAt,
        };
    }

    public async Task AddNew(TrackInsertModel model, string userId)
    {
        if (userId == null)
        {
            throw new AppException(HttpStatusCode.Unauthorized, "Cần tài khoản để upload nhạc", "Hãy đăng nhập lại");
        }
    
        var track = new Track()
        {
            Name = model.TrackName,
            Description = model.Description,
            ArtWork = model.ArtWork
        };
        var action = new UserTrackAction()
        {
            Track = track,
            UserId = userId,
            ActionType = EUserTrackActionType.UPLOAD,
            CreatedAt = DateTime.Now,
        };
        try 
        {
            await _context.Database.BeginTransactionAsync();
            await _context.Tracks.AddAsync(track);
            await _context.UserTrackActions.AddAsync(action);
            await _context.SaveChangesAsync();
        } catch (Exception)
        {
            _context.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<TrackUploadSuccessModel> UploadCache(IFormFile file)
    {
        if (file.FileName.EndsWith(".mp3"))
        {
            throw new AppException(HttpStatusCode.BadRequest, 
                "File không đúng định dạng", "File phải đuôi .mp3");
        }
        var location = Path.Combine("Assets", "musics", file.FileName);
        await file.CopyToAsync(new FileStream(location, FileMode.Create));
        return new TrackUploadSuccessModel()
        {
            TrackName = Path.GetFileNameWithoutExtension(file.FileName),
            UploadAt = DateTime.Now,
            ExpiredAt = DateTime.Now.AddMinutes(3)
        };
    }

    public async Task Remove(int id)
    {
        var track = await _context.Tracks
            .Include(t => t.UserActions)
            .Include(t => t.Comments)
            .Include(t => t.Categories)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không tìm thấy bài hát", 
                "Hãy thử tải lại trang. Luôn đảm bảo bạn có quyền xoá bài hát này");

        using var deleteTrackTransaction = _context.Database.BeginTransaction();
        try
        {
            _context.UserTrackActions.RemoveRange(track.UserActions);
            _context.Comments.RemoveRange(track.Comments);
            _context.Track_Categories.RemoveRange(track.Categories);
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();

            deleteTrackTransaction.Commit();
        } catch (Exception)
        {
            deleteTrackTransaction.Rollback();
            throw;
        }
    }
    
    public async Task UpdateInfomation(TrackUpdateModel model, int id)
    {
        var currentTrack = await _context.Tracks.FindAsync(id) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không tìm thấy bài hát", 
                "Hãy thử lại");
        
        currentTrack.Name = model.TrackName;
        currentTrack.Description = model.Description;
        currentTrack.ArtWork = model.ArtWork;
        if (model.Categories != null)
        {
            var currentCategories = await _context.Track_Categories
                .Where(tc => tc.TrackId == id)
                .ToListAsync();
            _context.Track_Categories.RemoveRange(currentCategories);
            var updateCategories = await _context.Categories
                .Where(c => model.Categories.Contains(c.Id))
                .ToListAsync();
            await _context.Track_Categories
                .AddRangeAsync(updateCategories.Select(c => new Track_Category()
                {
                    TrackId = id,
                    CategoryId = c.Id
                }));
        }
        await _context.SaveChangesAsync();
    }
}