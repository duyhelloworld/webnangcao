using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Entities;

public class UserPlaylistAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [EnumDataType(typeof(EUserPlaylistActionType))]
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