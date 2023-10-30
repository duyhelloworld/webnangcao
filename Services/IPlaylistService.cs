using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;

namespace webnangcao.Services;

public interface IPlaylistService
{
    public Task<IEnumerable<PlaylistResponseModel>> GetAll();
    public Task<PlaylistResponseModel?> GetById(int id);
    public Task<int> AddNew(PlaylistInsertModel model, string userId);
    public Task UpdateInfomation(PlaylistInsertModel model, int id);
    public Task Delete(int id);
}