namespace webnangcao.Models.Inserts;

public class TrackInsertModel
{
    public string TrackName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public IEnumerable<string>? Categories { get; set; }
}