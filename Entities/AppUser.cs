using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class AppUser : IdentityUser
{
    [Key]
    [StringLength(150)]
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Address { get; set; }
}