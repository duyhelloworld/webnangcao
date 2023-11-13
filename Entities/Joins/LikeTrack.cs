// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore;

// namespace webnangcao.Entities.Joins;
// [Table("Likes")]
// [PrimaryKey("Id")]
// public class Like
// {
//     public int Id { get; set; }
//     public long UserId { get; set; }
//     public int TrackId { get; set; }
//     public DateTime ActionAt { get; set; }
//     [ForeignKey("UserId")]
//     public virtual User User { get; set; }
//     [ForeignKey("TrackId")]
//     public virtual Track Track { get; set; }
// }
