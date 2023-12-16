using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Entities;

public class Track
{
    [Key]
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Track)]
    public string Name { get; set; } = null!;

    [Max(EMaxValue.DirectoryLength)]
    public string FileName { get; set; } = null!;

    [Max(EMaxValue.DirectoryLength)]
    public string ArtWork { get; set; } = null!;

    public DateTime UploadAt { get; set; }

    public bool IsPrivate { get; set; }

    public int ListenCount { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }

    [MaxLength]
    public string? Description { get; set; }

    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public User Author { get; set; } = null!;
}