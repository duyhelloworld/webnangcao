using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webnangcao.Entities;

public class UserTrackAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [EnumDataType(typeof(UserTrackAction), ErrorMessage = "Hành động không hợp lệ")]
    public UserTrackAction ActionName { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public int TrackId { get; set; }
    [ForeignKey("TrackId")]
    public Track Track { get; set; } = null!;
}