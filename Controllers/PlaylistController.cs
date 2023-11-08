using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Enumerables;
using webnangcao.Exceptions;
using webnangcao.Services;
using webnangcao.Tools;
using System.Net;
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
    public IActionResult GetAllAsync()
    {
        return Ok(_service.GetAllPublic());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
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

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        return Ok(await _service.Search(keyword));
    }

    [HttpGet("listen/{playlistId}")]
    public async Task UpdateListentCount(int playlistId)
    {
        await _service.UpdateListentCount(playlistId);
    }

    [HttpPost("{playlistId}/like")]
    [AppAuthorize(ERole.USER)]
    public async Task Like(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Like(playlistId, uid);
        }
        throw new AppException(HttpStatusCode.Forbidden, 
            "Phải có tài khoản mới được thực hiện hành động này", 
            "Hãy tạo tài khoản và trở lại sau");
    }
}