using System.Net;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Exceptions;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

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
    public async Task<IEnumerable<TrackResponseModel>> GetByUserId(int id)
    {
        var result = from track in _context.Tracks
            join userTrackAction in _context.UserTrackActions
                on track.Id equals userTrackAction.TrackId
            join user in _context.Users
                on userTrackAction.UserId equals user.Id
            where user.Id == id
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
    public async Task UploadTrack(TrackInsertModel model, long userId)
    {
        // JsonSerializer.Deserialize<TrackInsertModel>("");
        var track = new Track()
        {
            Name = model.TrackName,
            Description = model.Description,
            // ArtWork = model.ArtWork,
            // AudioFile = model.AudioFile.FileName,
            // ArtworkFile = model.ArtorkFile.FileName,
        };
        var userTrackAction = new UserTrackAction()
        {
            UserId = userId,
            Track = track,
            ActionAt = DateTime.Now,
        };
        _context.Tracks.Add(track);
        _context.UserTrackActions.Add(userTrackAction);
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
    
    public async Task UpdateInfomation(TrackUpdateModel model, int id)
    {
        var currentTrack = await _context.Tracks.FindAsync(id) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không tìm thấy bài hát", 
                "Hãy thử lại");
        currentTrack.Name = model.TrackName;
        currentTrack.Description = model.Description;
        currentTrack.ArtWork = model.ArtWork;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetByName(string name)
    {
        var result = from track in _context.Tracks
            join userTrackAction in _context.UserTrackActions
                on track.Id equals userTrackAction.TrackId
            join user in _context.Users
                on userTrackAction.UserId equals user.Id
            where track.Name.Contains(name)
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
        // Kiểm tra xem người dùng đã like bài hát này chưa
        bool isLiked = _context.UserTrackActions.Any(l => l.UserId == userId && l.TrackId == trackId);

        if (!isLiked)
        {
            // Nếu chưa like, thêm dữ liệu like vào database
            var like = new UserTrackAction { UserId = userId, TrackId = trackId };
            _context.UserTrackActions.Add(like);
        }
        else
        {
            // Nếu đã like, xóa dữ liệu like khỏi database
            var like = _context.UserTrackActions.FirstOrDefault(l => l.UserId == userId && l.TrackId == trackId);
            _context.UserTrackActions.Remove(like);
        }
        await _context.SaveChangesAsync();
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
}