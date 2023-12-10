using System.ComponentModel.DataAnnotations;
using webnangcao.Enumerables;
using webnangcao.Tools;

namespace webnangcao.Models.Inserts;

public class PlaylistInsertModel
{

    public int Id { get; set; } = 0;

    [Max(EMaxValue.NameLength_Playlist)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsPrivate { get; set; } = false;

    public string[]? Tags { get; set; }

    // Phải có ít nhất 1 bài hát mới tạo được playlist
    [MinLength(1)]
    public IEnumerable<int> TrackIds { get; set; } = null!;
}