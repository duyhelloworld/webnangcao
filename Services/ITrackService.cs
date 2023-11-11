using webnangcao.Entities;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Responses;
using webnangcao.Models.Updates;

namespace webnangcao.Services;

public interface ITrackService
{
    // Get public
    public Task<IEnumerable<TrackResponseModel>> GetTopNewest();
    public Task<IEnumerable<TrackResponseModel>> GetTopTrending();

    // Get private
    public Task<IEnumerable<TrackResponseModel>> GetAll(long userId);
    
    public Task<TrackResponseModel?> GetById(int id);
    
    public Task AddNew(TrackInsertModel model, IFormFile fileTrack, IFormFile fileArtwork, long userId);
    public Task UpdateInfomation(TrackUpdateModel model, IFormFile fileArtwork, int trackId);
    public Task Remove(int trackId);
}