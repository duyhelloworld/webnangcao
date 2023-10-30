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
    public Task AddNew(TrackInsertModel model, string userId);
    public Task<TrackUploadSuccessModel> UploadCache(IFormFile file);
    public Task UpdateInfomation(TrackUpdateModel model, int id);
    public Task Remove(int id);
}