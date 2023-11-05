using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webnangcao.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [MaxLength]
    public string Content { get; set; } = null!;

    public DateTime CommentAt { get; set; }
    public DateTime LastEditAt { get; set; }

    public int TrackId { get; set; }
    [ForeignKey("TrackId")]
    public Track Track { get; set; } = null!;

    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}