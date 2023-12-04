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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrackById(int id)
    {
        return Ok(await _service.GetById(id));
    }
    [HttpGet("user/{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetTrackByUserId(int id)
    {
        return Ok(await _service.GetByUserId(id));
    }
    [HttpGet("name/{name}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetTrackByName(string name)
    {
        return Ok(await _service.GetByName(name));
    }
    
    // [HttpPost("add")]
    // [AppAuthorize(ERole.USER)]
    // public async Task<IActionResult> Upload([FromForm] TrackInsertModel model)
    // {
    //     var model = JsonSerializer.Deserialize<TrackInsertModel>(modelstring);
    //     var userId = User.FindFirstValue("userid");
    //     if (userId != null && long.TryParse(userId, out long id) && model != null)
    //     {
    //         await _service.UploadTrack(model, id);
    //         return Ok();
    //     }
    //     return BadRequest();
    // }

    [HttpPut("update/{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Update(TrackUpdateModel model, IFormFile? fileArtwork, int trackId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id) && model != null)
        {
            await _service.UpdateInfomation(model, fileArtwork, trackId);
            return Ok();
        }
        return BadRequest();
    }
    [HttpPost("upload")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Upload([FromForm] string model, IFormFile fileTrack, IFormFile? fileArtwork)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id) && model != null)
        {
            var insertModel = JsonSerializer.Deserialize<TrackInsertModel>(model);
            await _service.UploadTrack(insertModel, id, fileTrack, fileArtwork);
            return Ok();
        }
        return Unauthorized();
    }
    
    [HttpGet("artwork/{filename}")]
    public IActionResult GetArtworkTrack([FromRoute] string filename)
    {
        var stream = FileTool.ReadTrack(filename);
        return new FileStreamResult(
            fileStream: stream,
            contentType: MediaTypeHeaderValue.Parse("audio/mpeg"));
    }



    // [HttpPut("{id}")]
    // [AppAuthorize(ERole.USER)]
    // public async Task<IActionResult> UpdateTrack(
    //     [FromForm] string modelstring,
    //     [FromForm] IFormFile? fileArtwork,
    //      int trackId)
    // {
    //     var model = JsonSerializer.Deserialize<TrackUpdateModel>(modelstring);
    //     var userId = User.FindFirstValue("userid");
    //     if (userId != null && long.TryParse(userId, out long id) && model != null)
    //     {
    //         await _service.UpdateInfomation(model, fileArtwork, trackId);
    //         return Ok();
    //     }
    //     return Forbid();
    // }
    [HttpDelete("delete/{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remove(id);
        return Ok();
    }
    [HttpPost("like/{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Like(int id)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && int.TryParse(userId, out int uid))
        {
            await _service.LikeTrack(uid, id);
            return Ok();
        }
        return BadRequest();
    }
}