using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Entities.Joins;

public class UserPlaylistAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime DateCreated { get; set; } = DateTime.Now;

    [Column(TypeName = "varchar")]
    [MaxLength((int)EMaxValue.ActionTypeNameLength)]
    public EUserPlayListActionType ActionType { get; set; }

    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User User { get; set; } = null!;

    public int PlaylistId { get; set; }
    [ForeignKey("PlaylistId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Playlist Playlist { get; set; } = null!;
}