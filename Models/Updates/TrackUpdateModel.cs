namespace webnangcao.Models.Updates;

public class TrackUpdateModel
{
    public string TrackName { get; set; } = null!;
    public string? Description { get; set; }
    public string? ArtWork { get; set; }
    public IEnumerable<int>? Categories { get; set; }
}