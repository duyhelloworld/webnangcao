using System.Net;
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
            join LikeTrack in _context.LikeTracks
                on track.Id equals LikeTrack.TrackId
            join user in _context.Users
                on LikeTrack.UserId equals user.Id
                join Comment in _context.Comments
                on track.Id equals Comment.TrackId
                join TrackCategory in _context.TrackCategories
                on track.Id equals TrackCategory.TrackId
                join Category in _context.Categories
                on TrackCategory.CategoryId equals Category.Id
            select new TrackResponseModel()
            {
                Id = track.Id,
                Author = user.UserName!,
                TrackName = track.Name,
                ArtWork = track.ArtWork,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount,
                Category = Category.Name,
                Comment = Comment.Content,
            };
        return await result.ToListAsync();
    }
    public async Task<IEnumerable<TrackResponseModel>> GetByUserId(int id)
    {
        var result = from track in _context.Tracks
            join user in _context.Users
                on track.Id equals user.Id
            where user.Id == id
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
        return await result.ToListAsync();
    }
    // public async Task UploadTrack(TrackInsertModel model, long userId)
    // {
    //     // JsonSerializer.Deserialize<TrackInsertModel>("");
    //     var track = new Track()
    //     {
    //         Name = model.TrackName,
    //         Description = model.Description,
    //         IsPrivate = model.IsPrivate,
    //     };
    //     if (model.Categories != null)
    //     {
    //         var categories = await _context.Categories
    //             .Where(c => model.Categories.Contains(c.Name))
    //             .ToListAsync();
    //         foreach (var category in categories)
    //         {
    //             await _context.TrackCategories.AddAsync(new TrackCategory()
    //             {
    //                 CategoryId = category.Id,
    //                 TrackId = track.Id,
    //             });
    //         }
    //     }
    //     if(fileArtwork != null)
    //     {
    //         await FileTool.SaveArtwork(fileArtwork);
    //         track.ArtWork = fileArtwork.FileName;
    //     }
    //     track.FileName = await FileTool.SaveTrack(fileTrack);
    //     track.UploadAt = DateTime.Now;
    //     await _context.Tracks.AddAsync(track);
    //     await _context.SaveChangesAsync();
    // }

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
            join LikeTrack in _context.LikeTracks
                on track.Id equals LikeTrack.TrackId
            join user in _context.Users
                on LikeTrack.UserId equals user.Id
            where track.Name.Contains(name)
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
        return await result.ToListAsync();
    }
    // public async Task PlayTrack(int id)
    // {
    //     var track = await _context.Tracks.FindAsync(id) 
    //         ?? throw new AppException(HttpStatusCode.NotFound, 
    //             "Không tìm thấy bài hát", 
    //             "Hãy thử lại");
    //     track.ListenCount++;
    //     await _context.SaveChangesAsync();
    // }
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
    public async Task CommentTrack(int userId, int trackId, string content)
    {
        var comment = new Comment { UserId = userId, TrackId = trackId, Content = content };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Comment>> GetComment(int trackId)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.TrackId == trackId)
            .ToListAsync();
        return comments;
    }
    public async Task EditComment(int id, string content)
    {
        var comment = await _context.Comments.FindAsync(id);
        comment.Content = content;
        await _context.SaveChangesAsync();
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
        return await result.FirstOrDefaultAsync();
    }
}