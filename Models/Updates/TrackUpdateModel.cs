using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class TrackUpdateModel
{
    [Max(EMaxValue.NameLength_Track)]
    public string TrackName { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }
}