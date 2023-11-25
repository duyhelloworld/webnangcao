using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using webnangcao.Enumerables;

namespace webnangcao.Entities;

public class UserPlaylistAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "varchar(10)")]
    public EUserPlaylistActionType ActionType { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ActionAt { get; set; }

    public long UserId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User User { get; set; } = null!;

    public int PlaylistId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Playlist Playlist { get; set; } = null!;
}