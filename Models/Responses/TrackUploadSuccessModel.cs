namespace webnangcao.Models.Responses;

public class TrackUploadSuccessModel
{
    public string TrackName { get; set; } = null!;
    public DateTime UploadAt { get; set; }
    public DateTime ExpiredAt { get; set; }
}