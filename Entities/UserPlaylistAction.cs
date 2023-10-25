using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Entities.Joins;

public class UserPlaylistAction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public int PlaylistId { get; set; }
    [ForeignKey("PlaylistId")]
    public Playlist Playlist { get; set; } = null!;

    public DateTime DateCreated { get; set; } = DateTime.Now;

    [EnumDataType(typeof(EUserPlayListAction), ErrorMessage = "Hành động không hợp lệ")]
    public EUserPlayListAction ActionName { get; set; }
}