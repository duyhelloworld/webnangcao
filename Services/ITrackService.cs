using webnangcao.Entities;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface ITrackService
{
    public Task<IEnumerable<TrackResponseModel>> GetAll();
    public Task<TrackResponseModel?> GetById(int id);
    // public Task<IEnumerable<TrackResponseModel>> Search(string input);
    
    public Task AddNew(TrackInsertModel model, long userId);
    public Task UpdateInfomation(TrackUpdateModel model, int id);
    public Task Remove(int id);
}