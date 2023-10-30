using System.ComponentModel.DataAnnotations;
using webnangcao.Entities.Enumerables;

namespace webnangcao.Models.Updates;

public class PlaylistUpdateModel
{
    public int Id { get; set; }

    [StringLength((int)EMaxValue.PlaylistNameLength)]
    public string Name { get; set; } = null!;

    public IFormFile? ArtWork { get; set; }

    public string? Description { get; set; }

    public IEnumerable<string>? Tags { get; set; }
}