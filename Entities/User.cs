using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using webnangcao.Entities.Enumerables;
using webnangcao.Entities.Joins;

namespace webnangcao.Entities;

public class User : IdentityUser
{
    [Key]
    [StringLength(150)]
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Address { get; set; }

    [StringLength((int)EMaxValue.AddressLength)]
    public string? FullName { get; set; }

    [StringLength((int)EMaxValue.DirectoryLength)]
    public string? AvatarDirectory { get; set; }

    public ICollection<Comment> Comments { get; set; } 
        = new HashSet<Comment>();

    public ICollection<UserTrackAction> TrackActions { get; set; } 
        = new HashSet<UserTrackAction>();

    public ICollection<UserPlaylistAction> Playlists { get; set; } 
        = new HashSet<UserPlaylistAction>();
}