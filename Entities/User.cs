using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class User : IdentityUser
{
    [Key]
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Address { get; set; }

    [StringLength((int)EMaxValue.AddressLength)]
    public string? FullName { get; set; }

    [StringLength((int)EMaxValue.DirectoryLength)]
    public string? Avatar { get; set; }


    public ICollection<Comment> Comments { get; set; } 
        = new HashSet<Comment>();

    public ICollection<UserTrackAction> TrackActions { get; set; } 
        = new HashSet<UserTrackAction>();

    public ICollection<Playlist> Playlists { get; set; } 
        = new HashSet<Playlist>();
}