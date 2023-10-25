using System.ComponentModel.DataAnnotations;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Track
{
    [Key]
    public int Id { get; set; }

    [StringLength((int)EMaxValue.TrackNameLength)]
    public string Name { get; set; } = null!;

    [StringLength((int)EMaxValue.DirectoryLength)]
    public string Directory { get; set; } = null!;


    [DataType(DataType.DateTime)]
    public DateTime UploadAt { get; set; } = DateTime.Now;

    [MaxLength]
    public string? Description { get; set; }


    [StringLength((int)EMaxValue.DirectoryLength)]
    public string? ArtWorkDirectory { get; set; }


    public ICollection<Track_Category> Categories { get; set; } 
        = new HashSet<Track_Category>();
    
    public ICollection<Comment> Comments { get; set; } 
        = new HashSet<Comment>();

    public ICollection<Track_Playlist> Playlists { get; set; } 
        = new HashSet<Track_Playlist>();
    public ICollection<UserTrackAction> UserActions { get; set; } 
        = new HashSet<UserTrackAction>();
}