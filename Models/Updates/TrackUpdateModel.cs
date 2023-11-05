using webnangcao.Entities.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class TrackUpdateModel
{
    [Max(EMaxValue.NameLength_Track)]
    public string TrackName { get; set; } = null!;

    public string? Description { get; set; }
    
    public string? ArtWork { get; set; }
}