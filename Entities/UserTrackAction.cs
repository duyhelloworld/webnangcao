using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using webnangcao.Enumerables;

namespace webnangcao.Entities;

public class UserTrackAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    // [EnumDataType(typeof(EUserTrackActionType))]
    [Column(TypeName = "varchar(10)")]
    public EUserTrackActionType ActionType { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime ActionAt { get; set; }

    public long UserId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User User { get; set; } = null!;

    public int TrackId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Track Track { get; set; } = null!;
}