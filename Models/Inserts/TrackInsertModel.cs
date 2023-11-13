namespace webnangcao.Models.Inserts;

public class TrackInsertModel
{
    public string TrackName { get; set; } = null!;
    public string TrackFileId { get; set; } = null!;
    public string? Description { get; set; }
    public string? ArtWork { get; set; }
    public string CategoryName { get; set; } = null!;
    public IEnumerable<string>? Tags { get; set; }
}