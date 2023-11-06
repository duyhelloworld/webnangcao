using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[Table("Tracks_Playlists")]
[PrimaryKey("PlaylistId", "TrackId")]
public class TrackPlaylist
{
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;

    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}