using System.ComponentModel.DataAnnotations;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Track
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Required]
    public string Directory { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime UploadAt { get; set; } = DateTime.Now;

    public string? Description { get; set; }

    public string? ArtWork { get; set; }

    // Cache lại để query nhanh hơn
    public int SoLuotNghe { get; set; }
    public int SoLuotThich { get; set; }
    public int SoLuotBinhLuan { get; set; }

    public ICollection<Track_Category> Categories { get; set; } = new HashSet<Track_Category>();
    public ICollection<Track_Playlist> Playlists { get; set; } = new HashSet<Track_Playlist>();
}