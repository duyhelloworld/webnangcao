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

    public async Task Remove(int id)
    {
        var track = _context.Tracks.Find(id);
        if (track == null)
        {
            throw new AppException(HttpStatusCode.NotFound, "Không tìm thấy bài hát", id);
        }
        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Track>> GetAll()
    {
        return await _context.Tracks.ToListAsync();
    }

    public async Task<IEnumerable<Track>> GetByName(string name)
    {
        return await _context.Tracks
        .Where(t => t.Name.Contains(name))
        .OrderBy(t => t.Name)
        .ToListAsync();
    }

    public async Task<Track?> GetById(int id)
    {
        return await _context.Tracks.FindAsync(id);
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