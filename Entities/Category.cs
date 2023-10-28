using System.ComponentModel.DataAnnotations;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength((int) EMaxValue.CategoryNameLength)]
    public string Name { get; set; } = null!;
    
    [MaxLength]
    public string? Description { get; set; }

    public ICollection<Track_Category> Tracks { get; set; } = new HashSet<Track_Category>();
}