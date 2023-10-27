using webnangcao.Entities;

namespace webnangcao.Services;

public interface ITrackService
{
    public Task<IEnumerable<Track>> GetAllAsync();
    public Task<Track> GetByIdAsync(int id);
    public Task<Track> CreateAsync(Track track);
    public Task<Track> UpdateAsync(Track track);
    public Task<Track> DeleteAsync(int id);
}