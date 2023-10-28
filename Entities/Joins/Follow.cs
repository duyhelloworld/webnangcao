using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[PrimaryKey("FollowedUserId", "FollowingUserId")]
public class Follow
{
    public string FollowingUserId { get; set; } = null!;
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User FollowingUser { get; set; } = null!;

    public string FollowedUserId { get; set; } = null!;
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User FollowedUser { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime StartedAt { get; set; } = DateTime.Now;
}