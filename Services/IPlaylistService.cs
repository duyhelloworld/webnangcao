using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;

namespace webnangcao.Services;

public interface IPlaylistService
{
    // Guest
    public IEnumerable<PlaylistResponseModel> GetAllPublic();
    public PlaylistResponseModel? GetById(int playlistId);
    public Task UpdateListentCount(int playlistId);
    public Task<IEnumerable<PlaylistResponseModel>> Search(string keyword);
    
    // User
    public Task<IEnumerable<PlaylistResponseModel>> GetAllByUser(long userId);
    public Task Like(int playlistId, long userId);
    public Task<int> AddNew(PlaylistInsertModel model, long userId);
    public Task SaveToLibrary(int playlistId, long userId);
    public Task UpdateInfomation(PlaylistInsertModel model, int playlistId, long userId);
    public Task Delete(int playlistId);
}