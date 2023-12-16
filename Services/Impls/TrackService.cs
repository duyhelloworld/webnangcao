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
    private readonly int PAGE_SIZE = 10;
    public TrackService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackResponseModel>> GetAllByAdmin(int page)
    {
        return await _context.Tracks
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .Select(track => new TrackResponseModel()
                {
                    Id = track.Id,
                    TrackName = track.Name,
                    Author = UserTool.GetAuthorName(track.Author),
                    FileName = track.FileName,
                    ArtWork = track.ArtWork,
                    Description = track.Description,
                    UploadAt = track.UploadAt,
                    ListenCount = track.ListenCount,
                    LikeCount = track.LikeCount,
                    CommentCount = track.CommentCount
                })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> SearchByAdmin(string input)
    {
        return await _context.Tracks
            .Where(t => t.Name.Contains(input) || t.Author.UserName!.Contains(input))
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetAllByGuestByUser(long uid, int page)
    {
        return await _context.Tracks
            .Where(t => t.AuthorId == uid || !t.IsPrivate)
            .OrderBy(t => t.Id)
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                IsPrivate = track.IsPrivate,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetAllUploadedByUser(long uid)
    {
        return await _context.Tracks
            .Where(t => t.AuthorId == uid)
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                IsPrivate = track.IsPrivate,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> GetAllByGuest(int page)
    {
        return await _context.Tracks
            .Where(t => !t.IsPrivate)
            .OrderBy(t => t.Id)
            .Skip((page - 1) * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                IsPrivate = track.IsPrivate,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> SearchByUser(string input, long userId)
    {
        return await _context.Tracks
            .Where(t => t.AuthorId == userId && (t.Name.Contains(input) || t.Author.UserName!.Contains(input)))
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TrackResponseModel>> SearchByGuest(string input)
    {
        return await _context.Tracks
            .Where(t => !t.IsPrivate && (t.Name.Contains(input) 
                || $"{t.Author.UserName} {t.Author.FirstName} {t.Author.LastName}".Contains(input)))
            .Select(track => new TrackResponseModel()
            {
                Id = track.Id,
                TrackName = track.Name,
                Author = UserTool.GetAuthorName(track.Author),
                FileName = track.FileName,
                ArtWork = track.ArtWork,
                Description = track.Description,
                UploadAt = track.UploadAt,
                ListenCount = track.ListenCount,
                LikeCount = track.LikeCount,
                CommentCount = track.CommentCount
            })
            .ToListAsync();
    }

    public async Task UpdateInfomation(TrackUpdateModel model, IFormFile? fileArtwork, int trackId, long userid)
    {
        var currentTrack = await _context.Tracks.FindAsync(trackId)
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát",
                "Hãy thử lại");
        if (currentTrack.AuthorId != userid)
        {
            throw new AppException(HttpStatusCode.Forbidden,
                "Bạn không có quyền chỉnh sửa bài hát này",
                "Hãy thử lại");
        }
        currentTrack.Name = model.TrackName;
        currentTrack.Description = model.Description;
        currentTrack.IsPrivate = model.IsPrivate;
        if (fileArtwork != null && fileArtwork.FileName != currentTrack.ArtWork)
        {
            await FileTool.SaveArtwork(fileArtwork);
            currentTrack.ArtWork = fileArtwork.FileName;
        }
        var currentCategories = await _context.TrackCategories
            .Where(tc => tc.TrackId == trackId)
            .ToListAsync();
        _context.TrackCategories.RemoveRange(currentCategories);
        if (model.CategoryIds != null)
        {
            foreach (var cid in model.CategoryIds)
            {
                if (_context.Categories.Any(p => p.Id == cid))
                {
                    await _context.TrackCategories
                        .AddAsync(new TrackCategory() 
                            { 
                                CategoryId = cid,
                                TrackId = trackId 
                            });
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task AddNew(TrackInsertModel model, long userId, IFormFile fileTrack, IFormFile? fileArtwork)
    {
        var track = new Track()
        {
            Name = model.TrackName,
            Description = model.Description,
            FileName = fileTrack.FileName,
            AuthorId = userId,
            UploadAt = DateTime.Now,
            IsPrivate = model.IsPrivate,
        };
        await FileTool.SaveTrack(fileTrack);
        if (fileArtwork != null)
        {
            await FileTool.SaveArtwork(fileArtwork);
            track.ArtWork = fileArtwork.FileName;
        }
        else
        {
            track.ArtWork = FileTool.DefaultArtWork;
        }
        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();
        if (model.CategoryIds != null)
        {
            foreach (var item in model.CategoryIds)
            {
                if (_context.Categories.Any(p => p.Id == item))
                {
                    await _context.TrackCategories.AddAsync(new TrackCategory() 
                        { 
                            CategoryId = item,
                            TrackId = track.Id
                        });
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
            currentTrack.ArtWork = fileArtwork.FileName;
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
        }
        else
        {
            _context.LikeTracks.Remove(like);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<Stream> PlayTrack(string fileName, long userId)
    {
        var track = await _context.Tracks
            .FirstOrDefaultAsync(t => t.FileName == fileName) ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy bài hát",
                "Hãy thử lại");
        if (track.IsPrivate)
        {
            if (track.AuthorId != userId)
            {
                throw new AppException(HttpStatusCode.Forbidden,
                    "Bạn không có quyền nghe bài hát này",
                    "Hãy thử lại");
            }
        }
        return FileTool.ReadTrack(fileName);
    }

    
}