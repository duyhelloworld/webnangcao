using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;
[Table("LikeTrack")]
[PrimaryKey("Id")]
public class LikeTrack
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public int TrackId { get; set; }
    [ForeignKey("UserId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual User User { get; set; }
    [ForeignKey("TrackId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Track Track { get; set; }
}
