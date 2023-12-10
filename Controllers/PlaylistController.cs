using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Enumerables;
using webnangcao.Services;
using webnangcao.Tools;
using webnangcao.Models.Updates;
using Microsoft.Net.Http.Headers;
namespace webnangcao.Controllers;

[Route("[controller]")]
[ApiController]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _service;
    public PlaylistController(IPlaylistService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRandomPlaylist()
    {
        return Ok(await _service.GetRandom());
    }

    [HttpGet("{playlistId}")]
    public async Task<IActionResult> GetPublicById(int playlistId)
    {
        var rs = await _service.GetPublicById(playlistId);
        if (rs != null)
        {
            return Ok(rs);
        }
        return NotFound();
    }

    [HttpGet("admin/all")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("user/all")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetAllByUser()
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            return Ok(await _service.GetAllByUser(uid));
        }
        return Forbid();
    }

    [HttpGet("user/{playlistId}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetOfUserById(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            var result = await _service.GetOfUserById(playlistId, uid);
            if (result == null) 
                return NotFound();
            return Ok(result);
        }
        return Forbid();
    }

    // [HttpGet("artwork/{filename}")]
    // public IActionResult GetArtwork(string filename)
    // {
    //     return new FileStreamResult(
    //         fileStream: FileTool.ReadArtWork(fileName: filename),
    //         contentType: new MediaTypeHeaderValue("image/jpeg"));        
    // }

    [HttpPost]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> AddNew(
        [FromForm] string playlistJson,
        [FromForm] IFormFile? fileArtwork)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            return Ok(await _service.AddNew(playlistJson, fileArtwork, uid));
        }
        return Forbid();
    }

    [HttpPut("{playlistId}/like")]
    [AppAuthorize(ERole.USER)]
    public async Task Like(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Like(playlistId, uid);
        }
    }

    [HttpPut("{playlistId}/repost")]
    [AppAuthorize(ERole.USER)]
    public async Task SaveToLibrary(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Repost(playlistId, uid);
        }
    }

    [HttpPut("{playlistId}/information")]
    [AppAuthorize(ERole.USER)]
    public async Task UpdateInfomation([FromBody] PlaylistUpdateModel model, IFormFile? fileArtwork, int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.UpdateInfomation(model, fileArtwork, uid);
        }
    }

    [HttpDelete("{playlistId}")]
    [AppAuthorize(ERole.USER)]
    public async Task Delete(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Delete(playlistId, uid);
        }
    }
}