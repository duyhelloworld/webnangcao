using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Entities.Joins;
using webnangcao.Exceptions;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
using webnangcao.Tools;

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
                     join user in _context.Users
                         on track.AuthorId equals user.Id
                     //  join LikeTrack in _context.LikeTracks
                     //     on track.Id equals LikeTrack.TrackId
                     //  join comment in _context.Comments 
                     //     on track.Id equals comment.TrackId
                     //  join trackCategory in _context.TrackCategories
                     //     on track.Id equals trackCategory.TrackId
                     //  join category in _context.Categories
                     //     on trackCategory.CategoryId equals category.Id
                     select new TrackResponseModel
                     {
                         Id = track.Id,
                         TrackName = track.Name,
                         Author = $"{user.FirstName} {user.LastName}",
                         FileName = track.FileName,
                         ArtWork = track.ArtWork,
                         Description = track.Description,
                         ListenCount = track.ListenCount,
                         LikeCount = track.LikeCount,
                         CommentCount = track.CommentCount,
                         //  Category = category.Name,
                         //  Comment = comment.Content
                     };

        return await result.ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetByUserId(int id)
    {
        var result = from track in _context.Tracks
                     join user in _context.Users
                         on track.AuthorId equals user.Id
                     where user.Id == id
                     select new TrackResponseModel()
                     {
                         Id = track.Id,
                         Author = $"{user.FirstName} {user.LastName}",
                         TrackName = track.Name,
                         FileName = track.FileName,
                         Description = track.Description,
                         ArtWork = track.ArtWork,
                         ListenCount = track.ListenCount,
                         LikeCount = track.LikeCount,
                         CommentCount = track.CommentCount,
                     };
        return await result.ToListAsync();
    }
    public async Task UploadTrack(TrackInsertModel model, long userId, IFormFile fileTrack, IFormFile? fileArtwork)
    {
        var track = new Track()
        {
            Name = model.TrackName,
            Description = model.Description,
            FileName = fileTrack.FileName,
            AuthorId = userId,
        };
        if (fileArtwork != null)
        {
            await FileTool.SaveArtwork(fileArtwork);
            track.ArtWork = fileArtwork.FileName;
        }
        else
        {
            track.ArtWork = "default-artwork.jpg";
        }
        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();
        if (model.CategoryIds != null)
        {
            foreach (var item in model.CategoryIds)
            {
                if (_context.Categories.Any(p => p.Id == item))
                {
                    await _context.TrackCategories.AddAsync(new TrackCategory() { CategoryId = item, TrackId = track.Id });
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateInfomation(TrackUpdateModel model, IFormFile? fileArtwork, int trackId)
    {
        var currentTrack = await _context.Tracks.FindAsync(trackId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát",
                "Hãy thử lại");

        currentTrack.Name = model.TrackName;
        currentTrack.Description = model.Description;
        if (fileArtwork != null && fileArtwork.FileName != currentTrack.ArtWork)
        {
            await FileTool.SaveArtwork(fileArtwork);
        }
        var currentCategories = await _context.TrackCategories
            .Where(tc => tc.TrackId == trackId)
            .ToListAsync();
        _context.TrackCategories.RemoveRange(currentCategories);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var track = await _context.Tracks.FindAsync(id)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát",
                "Hãy thử lại");
        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetByName(string name)
    {
        var result = from track in _context.Tracks
                     join user in _context.Users
                         on track.AuthorId equals user.Id
                     where track.Name.Contains(name)
                     select new TrackResponseModel()
                     {
                         Id = track.Id,
                         Author = $"{user.FirstName} {user.LastName}",
                         TrackName = track.Name,
                         FileName = track.FileName,
                         Description = track.Description,
                         ArtWork = track.ArtWork,
                         ListenCount = track.ListenCount,
                         LikeCount = track.LikeCount,
                         CommentCount = track.CommentCount,
                     };
        return await result.ToListAsync();
    }

    public async Task LikeTrack(int userId, int trackId)
    {
        var like = await _context.LikeTracks
            .FirstOrDefaultAsync(l => l.UserId == userId && l.TrackId == trackId);
        if (like == null)
        {
            await _context.LikeTracks.AddAsync(new LikeTrack()
            {
                UserId = userId,
                TrackId = trackId,
            });
            await _context.SaveChangesAsync();
        }
        else
        {
            _context.LikeTracks.Remove(like);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<TrackResponseModel> GetById(int id)
    {
        var result = from track in _context.Tracks
                     join LikeTrack in _context.LikeTracks
                         on track.Id equals LikeTrack.TrackId
                     join user in _context.Users
                         on LikeTrack.UserId equals user.Id
                     where track.Id == id
                     select new TrackResponseModel()
                     {
                         Id = track.Id,
                         Author = user.UserName!,
                         TrackName = track.Name,
                         ArtWork = track.ArtWork,
                         ListenCount = track.ListenCount,
                         LikeCount = track.LikeCount,
                         CommentCount = track.CommentCount,
                     };
        return await result.FirstOrDefaultAsync() ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát",
                "Hãy thử lại");
    }
    public async Task<IActionResult> PlayTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id)
                ?? throw new AppException(HttpStatusCode.NotFound,
                    "Không tìm thấy bài hát",
                    "Hãy thử lại");

            var filePath = Path.Combine("wwwroot/musics", track.FileName);

            if (!File.Exists(filePath))
            {
                throw new AppException(HttpStatusCode.NotFound,
                    "Không tìm thấy tệp âm thanh",
                    "Hãy thử lại");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(fileStream, "audio/mpeg")
            {
                EnableRangeProcessing = true
            };
        }

}