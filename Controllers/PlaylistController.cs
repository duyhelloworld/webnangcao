using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Enumerables;
using webnangcao.Exceptions;
using webnangcao.Services;
using webnangcao.Tools;
using System.Net;
using webnangcao.Models.Inserts;
using webnangcao.Models.Updates;
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

    [HttpGet("all")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("public")]
    public async Task<IActionResult> GetAllPublic()
    {
        return Ok(await _service.GetAllPublic());
    }

    [HttpGet("public/{playlistId}")]
    public async Task<IActionResult> GetPublicById(int playlistId)
    {
        var rs = await _service.GetPublicById(playlistId);
        if (rs != null)
        {
            return Ok(rs);
        }
        return NotFound();
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
            return Ok(await _service.GetOfUserById(playlistId, uid));
        }
        return Forbid();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        if (keyword == null)
        {
            return Ok(_service.GetAllPublic());
        }
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            return Ok(await _service.Search(keyword, uid));
        }
        return Ok(await _service.Search(keyword, null));
    }

    [HttpPut("{playlistId}/play")]
    public async Task Play(int playlistId)
    {
        await _service.Play(playlistId);
    }

    [HttpPost]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> AddNew([FromBody] PlaylistInsertModel model)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            var playlistId = await _service.AddNew(model, uid);
            return Ok(new{playlistId});
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

    [HttpPut("{playlistId}/save")]
    [AppAuthorize(ERole.USER)]
    public async Task SaveToLibrary(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.SaveToLibrary(playlistId, uid);
        }
    }

    [HttpPut("{playlistId}/information")]
    [AppAuthorize(ERole.USER)]
    public async Task UpdateInfomation([FromBody] PlaylistUpdateModel model, int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.UpdateInfomation(model, playlistId, uid);
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