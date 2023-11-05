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
        select new TrackResponseModel()
        {
            Id = track.Id,
            Author = user.UserName!,
            TrackName = track.Name,
            ArtWork = track.ArtWork,
            UploadAt = userTrackAction.ActionAt,
            ListenCount = track.ListenCount,
            LikeCount = track.LikeCount,
            CommentCount = track.CommentCount,
        };
        return await Task.FromResult(result);
    }

    public async Task<TrackResponseModel?> GetById(int id)
    {
        var result = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không thể tìm kiếm bài hát", 
                "Hãy thử kiểm tra đường truyền");
        return new TrackResponseModel()
        {
            Id = id,
            TrackName = result.Name,
            Author = result.Author ?? "Không rõ",
            ArtWork = result.ArtWork,
            UploadAt = result.UserTrackActions.FirstOrDefault()!.ActionAt,
            ListenCount = result.ListenCount,
            LikeCount = result.LikeCount,
            CommentCount = result.CommentCount,
        };
    }

    public async Task AddNew(TrackInsertModel model, long userId)
    {    
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
            ActionAt = DateTime.Now,
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

    public async Task Remove(int id)
    {
        var track = await _context.Tracks
            .Include(t => t.UserTrackActions)
            .Include(t => t.Comments)
            .Include(t => t.Categories)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không tìm thấy bài hát", 
                "Hãy thử tải lại trang. Luôn đảm bảo bạn có quyền xoá bài hát này");

        using var deleteTrackTransaction = _context.Database.BeginTransaction();
        try
        {
            _context.UserTrackActions.RemoveRange(track.UserTrackActions);
            _context.Comments.RemoveRange(track.Comments);
            _context.TrackCategories.RemoveRange(track.Categories);
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
        var currentCategories = await _context.TrackCategories
            .Where(tc => tc.TrackId == id)
            .ToListAsync();
        _context.TrackCategories.RemoveRange(currentCategories);
        await _context.SaveChangesAsync();
    }
}