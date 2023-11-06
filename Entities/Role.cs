using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace webnangcao.Entities;

public class Role : IdentityRole<long>
{
    [Required]
    public override string? Name { get ; set ; }
    public override string? NormalizedName { get ; set ; }
}