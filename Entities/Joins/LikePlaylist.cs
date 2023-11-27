using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;
[Table("LikePlaylist")]
[PrimaryKey("Id")]
public class LikePlaylist
{    
    public int Id { get; set; }
    public long UserId { get; set; }
    public int PlaylistId { get; set; }
    [ForeignKey("UserId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual User User { get; set; }
    [ForeignKey("PlaylistId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Playlist Playlist { get; set; }
}
