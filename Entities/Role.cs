using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Entities;

public class Role : IdentityRole
{
    [StringLength((int)EMaxValue.RoleLength)]
    public override string Id { get; set ; } = Guid.NewGuid().ToString();
}