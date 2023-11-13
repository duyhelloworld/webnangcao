using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webnangcao.Enumerables;
using webnangcao.Entities.Joins;
using webnangcao.Tools;

namespace webnangcao.Entities;

public class Track
{
    [Key]
    public int Id { get; set; }

    [Max(EMaxValue.NameLength_Track)]
    public string Name { get; set; } = null!;

    [Max(EMaxValue.DirectoryLength)]
    public string Location { get; set; } = null!;

    // Dùng UploadAt ở UserTrackAction

    [MaxLength]
    public string? Description { get; set; }

    [Max(EMaxValue.DirectoryLength)]
    public string? ArtWork { get; set; }

    public int ListenCount { get; set; } = 0;
    public int LikeCount { get; set; } = 0;
    public int CommentCount { get; set; } = 0;
    [NotMapped]
    public User Author { get; set; } = null!;
    public string AudioFile { get; set; }
    public string ArtworkFile { get; set; }
}