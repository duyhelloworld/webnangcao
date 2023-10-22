using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[PrimaryKey("NguoiFollowId", "NguoiDuocFollowId")]
public class Follow
{
    public string NguoiFollowId { get; set; } = null!;
    [ForeignKey("NguoiFollowId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public AppUser NguoiFollow { get; set; } = null!;

    public string NguoiDuocFollowId { get; set; } = null!;
    [ForeignKey("NguoiDuocFollowId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public AppUser NguoiDuocFollow { get; set; } = null!;
}