using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Enumerables;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;

namespace webnangcao.Services.Impl;

public class PlaylistService : IPlaylistService
{
    private readonly ApplicationContext _context;
    public PlaylistService(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<PlaylistResponseModel> GetAllPublic()
    {
        return from p in _context.Playlists
                    join tp in _context.TrackPlaylists on p.Id equals tp.PlaylistId
                    select new PlaylistResponseModel
                    {
                        Id = p.Id,
                        PlaylistName = p.Name,
                        AuthorName = p.Author.UserName!,
                        CreatedAt = p.CreatedAt,
                        Description = p.Description,
                        ArtWork = p.ArtWork,
                        Tracks = _context.TrackPlaylists
                                .Where(tp => tp.PlaylistId == p.Id)
                                .Select(tp => new TrackResponseModel()
                                {
                                    Id = tp.Track.Id,
                                    TrackName = tp.Track.Name,
                                    Author = tp.Track.Author.UserName!,
                                    ArtWork = tp.Track.ArtWork,
                                    ListenCount = tp.Track.ListenCount,
                                    CommentCount = tp.Track.CommentCount,
                                    LikeCount = tp.Track.LikeCount,
                                })
                                .ToList(),
                        Tags = p.Tags == null 
                            ? new string[] { } 
                            : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    }; 
    }

    public PlaylistResponseModel? GetById(int id)
    {
        var result = from p in _context.Playlists
                    where p.Id == id
                    select new PlaylistResponseModel
                    {
                        Id = p.Id,
                        PlaylistName = p.Name,
                        AuthorName = p.Author.UserName!,
                        CreatedAt = p.CreatedAt,
                        Description = p.Description,
                        ArtWork = p.ArtWork,
                        Tracks = _context.TrackPlaylists
                            .Where(tp => tp.PlaylistId == p.Id)
                            .Select(tp => new TrackResponseModel()
                            {
                                Id = tp.Track.Id,
                                TrackName = tp.Track.Name,
                                Author = tp.Track.Author.UserName!,
                                ArtWork = tp.Track.ArtWork,
                                ListenCount = tp.Track.ListenCount,
                                CommentCount = tp.Track.CommentCount,
                                LikeCount = tp.Track.LikeCount,
                            })
                            .ToList(),
                        Tags = p.Tags == null 
                            ? new string[] { } 
                            : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    };
        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) 
        {
            return Array.Empty<PlaylistResponseModel>();
        }
        return from p in _context.Playlists
                where p.AuthorId == userId
                select new PlaylistResponseModel
                {
                    Id = p.Id,
                    PlaylistName = p.Name,
                    AuthorName = p.Author.UserName!,
                    CreatedAt = p.CreatedAt,
                    Description = p.Description,
                    ArtWork = p.ArtWork,
                    Tracks = _context.TrackPlaylists
                        .Where(tp => tp.PlaylistId == p.Id)
                        .Select(tp => new TrackResponseModel()
                        {
                            Id = tp.Track.Id,
                            TrackName = tp.Track.Name,
                            Author = tp.Track.Author.UserName!,
                            ArtWork = tp.Track.ArtWork,
                            ListenCount = tp.Track.ListenCount,
                            CommentCount = tp.Track.CommentCount,
                            LikeCount = tp.Track.LikeCount,
                        })
                        .ToList(),
                    Tags = p.Tags == null
                        ? new string[] { }
                        : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
                };
    }

    public async Task<IEnumerable<PlaylistResponseModel>> Search(string keyword)
    {
        var query = from p in _context.Playlists
            where p.Name.Contains(keyword) || (p.Tags != null && p.Tags.Contains(keyword)) || (p.Description !=null && p.Description.Contains(keyword))
            select new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                Description = p.Description,
                ArtWork = p.ArtWork,
                Tracks = _context.TrackPlaylists
                        .Where(tp => tp.PlaylistId == p.Id)
                        .Select(tp => new TrackResponseModel()
                        {
                            Id = tp.Track.Id,
                            TrackName = tp.Track.Name,
                            Author = tp.Track.Author.UserName!,
                            ArtWork = tp.Track.ArtWork,
                            ListenCount = tp.Track.ListenCount,
                            CommentCount = tp.Track.CommentCount,
                            LikeCount = tp.Track.LikeCount,
                        })
                        .ToList(),
                Tags = p.Tags == null
                        ? new string[] { }
                        : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
            };
        return await query.ToListAsync();
    }

    public async Task Like(int playlistId, long userId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId);
        var user = await _context.Users.FindAsync(userId);
        if (playlist == null || user == null) 
            return;
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

    public async Task UpdateListentCount(int playlistId)
    {
        var playlist = await _context.Playlists.FindAsync(playlistId);
        if (playlist == null) 
            return;
        playlist.ListenCount++;
        await _context.SaveChangesAsync();
    }

    public Task<int> AddNew(PlaylistInsertModel model, long userId)
    {
        throw new NotImplementedException();
    }

    public Task SaveToLibrary(int playlistId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateInfomation(PlaylistInsertModel model, int playlistId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateInfomation(PlaylistInsertModel model, int id)
    {
        throw new NotImplementedException();
    }
    
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    } 
}