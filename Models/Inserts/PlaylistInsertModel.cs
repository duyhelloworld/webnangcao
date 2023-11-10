using System.ComponentModel.DataAnnotations;
using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Inserts;

public class PlaylistInsertModel
{
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Playlist)]
    public string Name { get; set; } = null!;

    public IFormFile? ArtWork { get; set; }

    public string? Description { get; set; }

    public bool IsPrivate { get; set; }

    public string[]? Tags { get; set; }
    
    public IEnumerable<int>? TrackIds { get; set; }
}