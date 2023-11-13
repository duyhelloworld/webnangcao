using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Models.Inserts;
using webnangcao.Models.Updates;
using webnangcao.Services;
namespace webnangcao.Controllers;

[ApiController]
[Route("[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackService _service;
    public TrackController(ITrackService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrack()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost("add")]
    public async Task Upload([FromForm] string modelstring, [FromForm] IFormFile fileTrack, [FromForm] IFormFile fileArtwork)
    {
        var model = JsonSerializer.Deserialize<TrackInsertModel>(modelstring);
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id) && model != null)
        {
            await _service.AddNew(model, fileTrack, fileArtwork, id);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrack(
        [FromForm] string modelstring,
        [FromForm] IFormFile? fileArtwork,
         int trackId)
    {
        var model = JsonSerializer.Deserialize<TrackUpdateModel>(modelstring);
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id) && model != null)
        {
            await _service.UpdateInfomation(model, fileArtwork, trackId);
            return Ok();
        }
        return Forbid();
    }
}