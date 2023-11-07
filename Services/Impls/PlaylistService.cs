using webnangcao.Context;
using webnangcao.Enumerables;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;

namespace webnangcao.Services.Impl;

public class PlaylistService : IPlaylistService
{
    private readonly ApplicationContext _context;
    private const int MaxPagePerRequest = 10;
    public PlaylistService(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<PlaylistResponseModel> GetAll(int page)
    {
        var result = from p in _context.Playlists
                    join tp in _context.TrackPlaylists on p.Id equals tp.PlaylistId
                    select new PlaylistResponseModel
                    {
                        Id = p.Id,
                        PlaylistName = p.Name,
                        AuthorName = p.Author.UserName!,
                        CreatedAt = p.CreatedAt,
                        LastUpdatedAt = p.LastUpdatedAt,
                        Description = p.Description,
                        ArtWork = p.ArtWork,
                        Tracks = p.TrackPlaylists.Select(tp => new TrackResponseModel() 
                        {
                            Id = tp.TrackId,
                            TrackName = tp.Track.Name,
                            Author = tp.Track.Author.UserName!,
                            CommentCount = tp.Track.CommentCount,
                            LikeCount = tp.Track.LikeCount,
                            ListenCount = tp.Track.ListenCount,
                            ArtWork = tp.Track.ArtWork,
                            // UploadAt = tp.Track.UserTrackActions
                            //     .Where(uta => uta.ActionType == EUserTrackActionType.UPLOAD)
                            //     .Select(uta => uta.ActionAt)
                            //     .FirstOrDefault(),
                        }),
                        Tags = p.Tags == null 
                            ? new string[] { } 
                            : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    }; 
        return result.Skip((page - 1) * MaxPagePerRequest)
                     .Take(MaxPagePerRequest);
    }

    public PlaylistResponseModel? GetById(int id)
    {
        return _context.Playlists
            .Where(p => p.Id == id)
            .Select(p => new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.Author.UserName!,
                CreatedAt = p.CreatedAt,
                LastUpdatedAt = p.LastUpdatedAt,
                Description = p.Description,
                ArtWork = p.ArtWork,
                // Tracks = p.TrackPlaylists.Select(t => t.TrackId),
                Tags = p.Tags == null 
                    ? new string[] { } 
                    : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
            })
            .FirstOrDefault();
    }

    public Task UpdateInfomation(PlaylistInsertModel model, int id)
    {
        throw new NotImplementedException();
    }
    
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId, int page)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PlaylistResponseModel>> Search(string keyword)
    {
        throw new NotImplementedException();
    }

    public Task Play(int playlistId)
    {
        throw new NotImplementedException();
    }

    public Task Like(int playlistId, long userId)
    {
        throw new NotImplementedException();
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
}