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
        var result = from playlists in _context.Playlists
                    join playlists_tracks in _context.Track_Playlists 
                        on playlists.Id equals playlists_tracks.PlaylistId
                    select new PlaylistResponseModel
                    {
                        Id = playlists.Id,
                        PlaylistName = playlists.Name,
                        AuthorName = playlists.CreateUser.UserName!,
                        CreatedAt = playlists.CreatedAt,
                        LastUpdatedAt = playlists.LastUpdatedAt,
                        Description = playlists.Description,
                        ArtWork = playlists.ArtWork,
                        Tracks = playlists.Tracks.Select(t => t.TrackId),
                        Tags = playlists.Tags == null 
                            ? new string[] { } 
                            : playlists.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries),
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
                AuthorName = p.CreateUser.UserName!,
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