using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Entities;

public class UserTrackAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    // [EnumDataType(typeof(EUserTrackActionType))]
    [Column(TypeName = "varchar(20)")]
    public EUserTrackActionType ActionType { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string UserId { get; set; } = null!;
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User User { get; set; } = null!;

    public int TrackId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Track Track { get; set; } = null!;
}