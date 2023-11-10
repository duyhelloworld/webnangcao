using Microsoft.EntityFrameworkCore;
using System.Net;
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
    private readonly string _artWorkPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "images");
    public PlaylistService(ApplicationContext context)
    {
        _context = context;
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
                Tracks = GetTracks(_context.TrackPlaylists, p.Id),
                ArtWork = ReadArtWork(p.ArtWork),
                Tags = GetTags(p.Tags),
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
                ArtWork = ReadArtWork(p.ArtWork),
                Tags = GetTags(p.Tags),
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
                ArtWork = ReadArtWork(playlist.ArtWork),
                Tracks = GetTracks(_context.TrackPlaylists, playlistId),
                Tags = GetTags(playlist.Tags)
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
                ArtWork = ReadArtWork(p.ArtWork),
                Tags = GetTags(p.Tags),
                Tracks = GetTracks(_context.TrackPlaylists, p.Id)
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
                Tracks = GetTracks(_context.TrackPlaylists, playlistId),
                AuthorId = result.AuthorId,
                Description = result.Description,
                ArtWork = ReadArtWork(result.ArtWork),
                Tags = GetTags(result.Tags)
            };  
    }

    public async Task<IEnumerable<PlaylistResponseModel>> Search(string keyword, long? userId)
    {
        var query = from p in _context.Playlists
            where (!p.IsPrivate || p.AuthorId == userId) &&
                (p.Name.Contains(keyword) ||
                (p.Tags != null && p.Tags.Contains(keyword)) ||
                (p.Description != null && p.Description.Contains(keyword)))
            select new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                ArtWork = ReadArtWork(p.ArtWork),
                Tracks = GetTracks(_context.TrackPlaylists, p.Id),
                Tags = GetTags(p.Tags)
            };
        return await query.ToListAsync();
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

    public async Task Play(int playlistId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId);
        if (playlist == null) 
            throw new AppException(HttpStatusCode.NotFound, 
                "Không thấy playlist yêu cầu");
        playlist.ListenCount++;
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddNew(PlaylistInsertModel model, long userId)
    {
        var playlist = new Playlist
        {
            Name = model.Name,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
            AuthorId = userId,
            IsPrivate = model.IsPrivate,
            Tags = SetTags(model.Tags),
        };
        // Chống 1 user tạo playlist trùng tên playlist của chính mình
        if (_context.Playlists.Any(p => p.Name == playlist.Name && p.AuthorId == userId))
            throw new AppException(HttpStatusCode.Conflict, 
                "Playlist này đã tồn tại");

        if (model.ArtWork != null)
        {
            var fileName = await WriteToDisk(model.ArtWork);
            playlist.ArtWork = fileName;
        }
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
        return  playlist.Id;
    }

    public async Task SaveToLibrary(int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId)
             ?? throw new AppException(HttpStatusCode.NotFound, 
            "Không thấy playlist yêu cầu");
        var playlistCopy = new Playlist
        {
            Name = playlist.Name,
            Description = playlist.Description,
            CreatedAt = playlist.CreatedAt,
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

    public async Task UpdateInfomation(PlaylistUpdateModel model, int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId) 
            ?? throw new AppException(HttpStatusCode.NotFound, 
                "Không thấy playlist yêu cầu");
        if (playlist.AuthorId != userId)
            throw new AppException(HttpStatusCode.Forbidden, 
                "Bạn không có quyền chỉnh sửa playlist này");
        playlist.Name = model.Name;
        playlist.Description = model.Description;
        playlist.Tags = SetTags(model.Tags);
        if (model.ArtWork != null)
        {
            var fileName = await WriteToDisk(model.ArtWork);
            playlist.ArtWork = fileName;
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




    private Stream ReadArtWork(string? artWork)
    {
        if (string.IsNullOrWhiteSpace(artWork))
            return Stream.Null;
        return new FileStream(Path.Combine(_artWorkPath, artWork), FileMode.Open);
    }

    private async Task<string> WriteToDisk(IFormFile file)
    {
        var fileName = file.FileName;
        var filePath = Path.Combine(_artWorkPath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return fileName;
    }

    private static string[] GetTags(string? tags)
    {
        if (string.IsNullOrWhiteSpace(tags))
            return Array.Empty<string>();
        return tags.Split(",", StringSplitOptions.RemoveEmptyEntries);
    }

    private static string? SetTags(string[]? tags)
    {
        if (tags == null)
            return null;
        return string.Join(",", tags);
    }

    private static IEnumerable<TrackResponseModel> GetTracks(DbSet<TrackPlaylist> trackPlaylists, int playlistId)
    {
        return trackPlaylists
            .Where(tp => tp.PlaylistId == playlistId)
            .Select(tp => new TrackResponseModel
            {
                Id = tp.Track.Id,
                TrackName = tp.Track.Name,
                Author = UserTool.GetAuthorName(tp.Track.Author),
                ArtWork = tp.Track.ArtWork,
                LikeCount = tp.Track.LikeCount,
            });
    }
}