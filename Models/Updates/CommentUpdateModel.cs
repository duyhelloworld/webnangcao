using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Updates;

public class CommentUpdateModel
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
}