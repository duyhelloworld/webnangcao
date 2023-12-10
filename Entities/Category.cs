using System.ComponentModel.DataAnnotations;
using webnangcao.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Tools;

namespace webnangcao.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Category)]
    public string Name { get; set; } = null!;
    
    [MaxLength]
    public string? Description { get; set; }
}