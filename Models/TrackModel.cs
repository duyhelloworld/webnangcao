namespace webnangcao.Models;

public class TrackModel
{
    public int Id { get; set; }
    public string TrackName { get; set; } = null!;
    public FileStream FileLocation { get; set; } = null!;
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? ArtWord { get; set; }
}