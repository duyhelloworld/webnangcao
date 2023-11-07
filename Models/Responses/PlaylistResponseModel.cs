namespace webnangcao.Models.Responses;

public class PlaylistResponseModel
{
    public int Id { get; set; }
    public string PlaylistName { get; set; } = null!;
    public string AuthorName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public string? Description { get; set; }

    public string? ArtWork { get; set; }

    public string[]? Tags { get; set; }
    public IEnumerable<TrackResponseModel>? Tracks { get; set; }
}