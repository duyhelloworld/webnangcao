using webnangcao.Entities;
namespace webnangcao.Models.Responses;

public class TrackResponseModel
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string TrackName { get; set; } = null!;
    public DateTime UploadAt { get; set; }
    public int ListenCount { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public string? ArtWork { get; set; }
}