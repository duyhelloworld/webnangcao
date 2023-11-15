using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using webnangcao.Enumerables;
using webnangcao.Models.Inserts;
using webnangcao.Models.Updates;
using webnangcao.Services;
using webnangcao.Tools;
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

    [HttpGet("{trackId}/stream")]
    public async Task<IActionResult> StreamTrack([FromRoute] int trackId)
    {
        var track = await _service.GetById(trackId);
        var stream = FileTool.ReadTrack(track?.FileName);
        return new FileStreamResult(
            fileStream: stream,
            contentType: MediaTypeHeaderValue.Parse("audio/mpeg"));
    }

    [HttpGet("artwork/{filename}")]
    public IActionResult GetArtworkTrack([FromRoute] string filename)
    {
        var stream = FileTool.ReadTrack(filename);
        return new FileStreamResult(
            fileStream: stream,
            contentType: MediaTypeHeaderValue.Parse("audio/mpeg"));
    }



    [HttpPut("{id}")]
    [AppAuthorize(ERole.USER)]
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