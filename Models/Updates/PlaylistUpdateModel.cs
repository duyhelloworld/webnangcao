using webnangcao.Entities.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class PlaylistUpdateModel
{
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Playlist)]
    public string Name { get; set; } = null!;

    public string? ArtWork { get; set; }

    public string? Description { get; set; }

    public IEnumerable<string>? Tags { get; set; }
}