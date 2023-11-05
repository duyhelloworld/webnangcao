using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Tools;

namespace webnangcao.Entities;

public class Track
{
    [Key]
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Track)]
    public string Name { get; set; } = null!;

    [Max(EMaxValue.DirectoryLength)]
    public string Location { get; set; } = null!;

    // Dùng UploadAt ở UserTrackAction

    [MaxLength]
    public string? Description { get; set; }

    [Max(EMaxValue.DirectoryLength)]
    public string? ArtWork { get; set; }

    [NotMapped]
    public int ListenCount { get; set; } = 0;
    [NotMapped]
    public int LikeCount { get; set; } = 0;
    [NotMapped]
    public int CommentCount { get; set; } = 0;
    [NotMapped]
    public string? Author { get; set; }

    public ICollection<TrackCategory> Categories { get; set; } 
        = new HashSet<TrackCategory>();
    
    public ICollection<Comment> Comments { get; set; } 
        = new HashSet<Comment>();

    public ICollection<TrackPlaylist> Playlists { get; set; } 
        = new HashSet<TrackPlaylist>();
        
    public ICollection<UserTrackAction> UserTrackActions { get; set; } 
        = new HashSet<UserTrackAction>();
}