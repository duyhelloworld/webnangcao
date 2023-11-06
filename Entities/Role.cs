using Microsoft.AspNetCore.Identity;

namespace webnangcao.Entities;

public class Role : IdentityRole<long>
{
    public override string Name { get ; set ; }  = null!;
    public override string? NormalizedName { get ; set ; }
}