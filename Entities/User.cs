using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webnangcao.Entities.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Entities;
public class User : IdentityUser<long>
{
    public override long Id { get; set; }

    [Required]
    public override string? UserName  { get; set; } 
    [Required]
    public override string? Email { get; set ; } 
    [Required]
    public override string? PasswordHash { get; set; } 

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Max(EMaxValue.DirectoryLength)]
    public string? Avatar { get; set; }

    public ICollection<Comment> Comments { get; set; } 
        = new HashSet<Comment>();

    public ICollection<UserTrackAction> UserTrackActions { get; set; } 
        = new HashSet<UserTrackAction>();

    public ICollection<Playlist> Playlists { get; set; } 
        = new HashSet<Playlist>();
}