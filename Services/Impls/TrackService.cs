
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;

namespace webnangcao.Services.Impls;

public class TrackService : ITrackService
{
    private readonly ApplicationContext _context;

    public TrackService(ApplicationContext context)
    {
        _context = context;
    }
    public Task<Track> CreateAsync(Track track)
    {
        throw new NotImplementedException();
    }

    public Task<Track> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Track>> GetAllAsync()
    {
        return await _context.Tracks.ToListAsync();
    }

    public Task<Track> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Track> UpdateAsync(Track track)
    {
        throw new NotImplementedException();
    }
}