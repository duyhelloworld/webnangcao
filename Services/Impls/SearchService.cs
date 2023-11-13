
using Microsoft.EntityFrameworkCore;
using webnangcao.Context;
using webnangcao.Models.Responses;
using webnangcao.Tools;

namespace webnangcao;

public class SearchService : ISearchService
{
    private readonly ApplicationContext _context;

    public SearchService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> Search(string keyword)
    {
        long userId = 0;
        var query = from p in _context.Playlists
                where (!p.IsPrivate || p.AuthorId == userId) &&
                    (p.Name.Contains(keyword) ||
                    (p.Tags != null && p.Tags.Contains(keyword)) ||
                    (p.Description != null && p.Description.Contains(keyword)))
                select new PlaylistResponseModel
                {
                    Id = p.Id,
                    PlaylistName = p.Name,
                    AuthorName = p.Author.UserName!,
                    CreatedAt = p.CreatedAt,
                    Description = p.Description,
                    ArtWork = FileTool.ReadArtWork(p.ArtWork),
                };
        return await query.ToListAsync();
    }
}