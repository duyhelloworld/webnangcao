using webnangcao.Context;
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
    public async Task<IEnumerable<PlaylistResponseModel>> GetAll()
    {
        var result = from p in _context.Playlists
                    join tp in _context.TrackPlaylists 
                        on p.Id equals tp.PlaylistId
                    select new PlaylistResponseModel
                    {
                        Id = p.Id,
                        PlaylistName = p.Name,
                        AuthorName = p.UserPlaylistActions.FirstOrDefault().User.UserName,
                        CreatedAt = p.CreatedAt,
                        LastUpdatedAt = p.LastUpdatedAt,
                        Description = p.Description,
                        ArtWork = p.ArtWork,
                        Tracks = p.Tracks.Select(t => t.TrackId),
                        Tags = p.Tags == null 
                            ? new string[] { } 
                            : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
                    }; 
        return await Task.FromResult(result);
    }

    public async Task<PlaylistResponseModel?> GetById(int id)
    {
        return await Task.FromResult(_context.Playlists
            .Where(p => p.Id == id)
            .Select(p => new PlaylistResponseModel
            {
                Id = p.Id,
                PlaylistName = p.Name,
                AuthorName = p.UserPlaylistActions.FirstOrDefault().User.UserName,
                CreatedAt = p.CreatedAt,
                LastUpdatedAt = p.LastUpdatedAt,
                Description = p.Description,
                ArtWork = p.ArtWork,
                Tracks = p.Tracks.Select(t => t.TrackId),
                Tags = p.Tags == null 
                    ? new string[] { } 
                    : p.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
            })
            .FirstOrDefault());
    }

    public Task<int> AddNew(PlaylistInsertModel model, string userId)
    {
        throw new NotImplementedException();
    }

    public Task AddNewTrackToPlaylist(int playlistId, int trackId)
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


    public Task RemoveTrackFromPlaylist(int playlistId, int trackId)
    {
        throw new NotImplementedException();
    }

}