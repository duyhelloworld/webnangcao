using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Playlist
{
    [Key]
    public int Id { get; set; }

    [StringLength((int) EMaxValue.PlaylistNameLength)]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    [MaxLength]
    public string? Description { get; set; }

    [StringLength((int)EMaxValue.DirectoryLength)]
    public string? ArtWorkDirectory { get; set; }

    public string CreateUserId { get; set; } = null!;
    [ForeignKey("CreateUserId")]
    public User CreateUser { get; set; } = null!;

    [MaxLength]
    public string? Tags { get; set; }

    public ICollection<Track_Playlist> Tracks { get; set; } 
        = new HashSet<Track_Playlist>();

    public ICollection<UserPlaylistAction> Users { get; set; } 
        = new HashSet<UserPlaylistAction>();
}