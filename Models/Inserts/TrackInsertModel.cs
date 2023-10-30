namespace webnangcao.Models.Inserts;

public class TrackInsertModel
{
    public string TrackName { get; set; } = null!;
    public string TrackFileId { get; set; } = null!;
    public string? Description { get; set; }
    public string? ArtWork { get; set; }
}