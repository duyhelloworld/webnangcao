using System.ComponentModel.DataAnnotations;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }

    public ICollection<Track_Category> Tracks { get; set; } = new HashSet<Track_Category>();
}