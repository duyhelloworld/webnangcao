using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Entities.Joins;
using webnangcao.Enumerables;
using webnangcao.Exceptions;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;
using webnangcao.Tools;

namespace webnangcao.Services.Impl;

public class PlaylistService : IPlaylistService
{
    private readonly ApplicationContext _context;
    public PlaylistService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PlaylistResponseModel>> GetRandom()
    {
        // Hiển thị 5 playlist ngẫu nhiên 
        return await _context.Playlists.Select(p => new PlaylistResponseModel
        {
            Id = p.Id,
            PlaylistName = p.Name,
            AuthorName = p.Author.UserName!,
            CreatedAt = p.CreatedAt,
            Description = p.Description,            
            ArtWork = FileTool.PlaylistArtWorkBaseUrl(p.ArtWork),
            Tags = TagTool.GetTags(p.Tags),
        }).OrderBy(p => Guid.NewGuid()).Take(5).ToListAsync();
    }

    public async Task<IEnumerable<PlaylistResponseModel>> GetAll()
    {
        return await _context.Playlists
            .Select(p => new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                LikeCount = p.LikeCount,
                RepostCount = p.RepostCount,
                IsPrivate = p.IsPrivate,
                ArtWork = FileTool.PlaylistArtWorkBaseUrl(p.ArtWork),
                Tags = TagTool.GetTags(p.Tags),
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<PlaylistResponseModel>> GetAllPublic()
    {
        return await _context.Playlists
            .Where(p => !p.IsPrivate)
            .Select(p => new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                ArtWork = FileTool.PlaylistArtWorkBaseUrl(p.ArtWork),
                Tags = TagTool.GetTags(p.Tags),
            })
            .ToListAsync();
    }

    public async Task<PlaylistResponseModel?> GetPublicById(int playlistId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId) 
            ?? throw new AppException(HttpStatusCode.NotFound,
                "Không tìm thấy playlist yêu cầu");
        if (playlist.IsPrivate)
            throw new AppException(HttpStatusCode.Forbidden,
                "Playlist này không được phép truy cập");
            
        return new PlaylistResponseModel
            {
                Id = playlistId,
                AuthorId = playlist.AuthorId,
                PlaylistName = playlist.Name,
                AuthorName = UserTool.GetAuthorName(playlist.Author),
                CreatedAt = playlist.CreatedAt,
                Description = playlist.Description,
                ArtWork = FileTool.PlaylistArtWorkBaseUrl(playlist.ArtWork ?? "default.png"),
                Tags = TagTool.GetTags(playlist.Tags),
                TrackIds = _context.TrackPlaylists
                    .Where(tp => tp.PlaylistId == playlistId)
                    .Select(tp => tp.TrackId)
                    .ToList()
            };
    }

    public async Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId)
    {
        return await _context.Playlists
            .Where(p => p.AuthorId == userId)
            .Select(p => new PlaylistResponseModel
            {
                Id = p.Id,
                AuthorId = p.AuthorId,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                ArtWork = FileTool.PlaylistArtWorkBaseUrl(p.ArtWork),
                Tags = TagTool.GetTags(p.Tags),
            })
            .ToListAsync();
    }

    public async Task<PlaylistResponseModel?> GetOfUserById(int playlistId, long userId)
    {
        var result = await _context.Playlists
            .Where(p => p.AuthorId == userId && p.Id == playlistId)
            .FirstOrDefaultAsync()
                ?? throw new AppException(HttpStatusCode.NotFound, 
                    "Không tìm thấy playlist yêu cầu");
        return new PlaylistResponseModel
            {
                Id = result.Id,
                PlaylistName = result.Name,
                AuthorName = UserTool.GetAuthorName(result.Author),
                CreatedAt = result.CreatedAt,
                AuthorId = result.AuthorId,
                Description = result.Description,
                ArtWork = FileTool.PlaylistArtWorkBaseUrl(result.ArtWork),
                Tags = TagTool.GetTags(result.Tags),
                TrackIds = await _context.TrackPlaylists
                    .Where(tp => tp.PlaylistId == playlistId)
                    .Select(tp => tp.TrackId)
                    .ToListAsync()
        };  
    }

    public async Task Like(int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không thấy playlist yêu cầu");
        var like = await _context.UserPlaylistActions.FindAsync(playlistId, userId);
        if (like == null)
        {
            await _context.UserPlaylistActions.AddAsync(new UserPlaylistAction
            {
                PlaylistId = playlistId,
                UserId = userId,
                ActionType = EUserPlaylistActionType.LIKE,
                ActionAt = DateTime.UtcNow
            });
            playlist.LikeCount++;
        }
        else
        {
            _context.UserPlaylistActions.Remove(like);
            playlist.LikeCount--;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddNew(string playlistJson, IFormFile? artwork, long userId)
    {
        var model = JsonSerializer.Deserialize<PlaylistInsertModel>(playlistJson);
        if (model == null)
            throw new AppException(HttpStatusCode.BadRequest, 
                "Dữ liệu không hợp lệ");
        var playlist = new Playlist
        {
            Name = model.Name,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
            AuthorId = userId,
            IsPrivate = model.IsPrivate,
            Tags = TagTool.SetTags(model.Tags),
        };
        // Chống 1 user tạo playlist trùng tên playlist của chính mình
        if (_context.Playlists.Any(p => p.AuthorId == userId && p.Name == playlist.Name))
            throw new AppException(HttpStatusCode.Conflict, 
                "Playlist này đã tồn tại");

        if (artwork != null)
        {
            await FileTool.SaveArtwork(artwork);
            playlist.ArtWork = artwork.FileName;
        }
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
        return  playlist.Id;
    }

    public async Task Repost(int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId)
             ?? throw new AppException(HttpStatusCode.NotFound, 
            "Không thấy playlist yêu cầu");
        playlist.RepostCount++;
        var playlistCopy = new Playlist
        {
            Name = playlist.Name,
            Description = playlist.Description,
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            IsPrivate = playlist.IsPrivate,
            Tags = playlist.Tags,
        };
        await _context.Playlists.AddAsync(playlistCopy);
        await _context.UserPlaylistActions.AddAsync(new UserPlaylistAction
        {
            PlaylistId = playlistCopy.Id,
            UserId = userId,
            ActionType = EUserPlaylistActionType.SAVETOLIBRARY,
            ActionAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateInfomation(PlaylistUpdateModel model, IFormFile? artwork, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(model.Id) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không thấy playlist yêu cầu");
        if (playlist.AuthorId != userId)
            throw new AppException(HttpStatusCode.Forbidden, 
                "Bạn không có quyền chỉnh sửa playlist này");
        playlist.Name = model.Name;
        playlist.Description = model.Description;
        playlist.Tags = TagTool.SetTags(model.Tags);
        if (artwork != null)
        {
            await FileTool.SaveArtwork(artwork);
            playlist.ArtWork = artwork.FileName;
        }
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không thấy playlist yêu cầu");
        if (playlist.IsPrivate)
            throw new AppException(HttpStatusCode.Forbidden,
                "Bạn không thể xóa playlist này");
        if (playlist.AuthorId != userId)
            throw new AppException(HttpStatusCode.Forbidden, 
                "Bạn không có quyền xóa playlist này");
        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();
    }   
}