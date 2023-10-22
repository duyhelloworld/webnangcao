using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[PrimaryKey("PlaylistId", "TrackId")]
public class Track_Playlist
{
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;

    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}