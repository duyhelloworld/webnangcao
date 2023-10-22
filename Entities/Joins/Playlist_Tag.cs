using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webnangcao.Entities.Joins;

public class Playlist_Tag
{
    [Key]
    public int PlaylistId { get; set; }
    [ForeignKey("PlaylistId")]
    public Playlist Playlist { get; set; } = null!;
    
    [StringLength(40)]
    public string Tag { get; set; } = null!;
}