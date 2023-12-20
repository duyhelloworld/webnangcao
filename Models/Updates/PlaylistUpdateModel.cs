using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class PlaylistUpdateModel
{    
    [Max(EMaxValue.NameLength_Playlist)]
    public string Name { get; set; } = null!;
    public bool IsPrivate { get; set; }
    public string? Description { get; set; }
    public IEnumerable<int>? TrackIds { get; set; }
    public string? Tags { get; set; }
}