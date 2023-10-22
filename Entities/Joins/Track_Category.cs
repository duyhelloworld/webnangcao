using Microsoft.EntityFrameworkCore;

namespace webnangcao.Entities.Joins;

[PrimaryKey("CategoryId", "TrackId")]
public class Track_Category
{
    public int TrackId { get; set; }
    public Track Track { get; set; } = null!;
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}