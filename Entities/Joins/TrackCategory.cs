using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[Table("Track_Category")]
[PrimaryKey("CategoryId", "TrackId")]
public class TrackCategory
{
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}