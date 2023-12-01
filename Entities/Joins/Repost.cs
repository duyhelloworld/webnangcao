using System.ComponentModel.DataAnnotations;

namespace webnangcao.Entities.Joins;

public class Repost
{
    [Key]
    public int Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}