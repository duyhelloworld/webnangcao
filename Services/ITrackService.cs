using webnangcao.Entities;

namespace webnangcao.Services;

public interface ITrackService
{
    Task<IEnumerable<Track>> GetAll();
    Task<Track?> GetById(int id);
    Task<int> AddNew(Track track);
    Task UploadTrack(IFormFile file);
    Task UpdateInfomation(Track track);
    Task Remove(int id);
}