using System.Net;
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Entities;
using webnangcao.Exceptions;

namespace webnangcao.Services.Impls;

public class TrackService : ITrackService
{
    private readonly ApplicationContext _context;

    public TrackService(ApplicationContext context)
    {
        _context = context;
    }
    public Task<Track> AddNew(Track track)
    {
        throw new NotImplementedException();
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Track>> GetAll()
    {
        return await _context.Tracks.ToListAsync();
    }

    public Task<Track?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Track> Update(Track track)
    {
        throw new NotImplementedException();
    }

    Task<int> ITrackService.AddNew(Track track)
    {
        throw new NotImplementedException();
    }

    public async Task UploadTrack(IFormFile file)
    {
        if (!file.FileName.EndsWith(".mp3"))
        {
            throw new AppException(HttpStatusCode.NotAcceptable, "File phải định dạng mp3", file.FileName);
        }
        await file.CopyToAsync(new FileStream(Path.Combine("Assets", "musics", file.FileName), FileMode.Create));
    }

    public Task UpdateInfomation(Track track)
    {
        throw new NotImplementedException();
    }
}