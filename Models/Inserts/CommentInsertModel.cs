using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class CommentInsertModel
{
    public int TrackId { get; set; }
    public string Content { get; set; } = null!;
}