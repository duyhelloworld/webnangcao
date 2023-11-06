using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Tools;

namespace webnangcao.Entities;

public class Playlist
{
    [Key]
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Playlist)]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsPrivate { get; set; }

    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public User Author { get; set; } = null!;

    [MaxLength]
    public string? Description { get; set; }

    [Max(EMaxValue.DirectoryLength)]
    public string? ArtWork { get; set; }
    

    [MaxLength]
    public string? Tags { get; set; }

    public ICollection<TrackPlaylist> Tracks { get; set; } 
        = new List<TrackPlaylist>();
    
    public ICollection<UserPlaylistAction> UserPlaylistActions { get; set; }
        = new List<UserPlaylistAction>();
}