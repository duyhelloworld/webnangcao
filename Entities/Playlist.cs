using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Playlist
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public DateTime ThoiGianTao { get; set; }
    public DateTime LanSuaCuoi { get; set; } = DateTime.Now;

    public string? Description { get; set; }
    public string? ArtWork { get; set; }

    public string NguoiTaoId { get; set; } = null!;
    [ForeignKey("NguoiTaoId")]
    public AppUser NguoiTao { get; set; } = null!;

    public ICollection<Playlist_Tag> Tags { get; set; } = new HashSet<Playlist_Tag>();
    public ICollection<Track_Playlist> CacTrack { get; set; } = new HashSet<Track_Playlist>();
}