using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Enumerables;
using webnangcao.Services;
using webnangcao.Tools;
using webnangcao.Models.Updates;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using webnangcao.Exceptions;
using System.Net;
using webnangcao.Models.Inserts;
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
    public async Task<IActionResult> GetPublic([FromQuery] int page)
    {
        if (page < 1)
        {
            page = 1;
        }
        return Ok(await _service.GetAllPublic(page));
    }

    [HttpGet("{playlistId}")]
    public async Task<IActionResult> GetPublicById(int playlistId)
    {
        var rs = await _service.GetPublicById(playlistId);
        if (rs != null)
        {
            return Ok(rs);
        }
        return NotFound("Không tìm thấy playlist này");
    }

    [HttpGet("admin/all")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllPublicAndPrivate());
    }

    [HttpGet("user/all")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetAllByUser()
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            return Ok(await _service.GetAllPlaylistCreatedByUser(uid));
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
            var result = await _service.GetByIdInUserCreatedPlaylist(playlistId, uid);
            if (result == null) 
                return NotFound();
            return Ok(result);
        }
        return Forbid();
    }

    [HttpGet("artwork/{filename}")]
    public IActionResult GetArtwork(string filename)
    {
        return new FileStreamResult(
            fileStream: FileTool.ReadArtwork(filename),
            contentType: new MediaTypeHeaderValue("image/jpeg"));        
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        return Ok(await _service.Search(keyword));
    }

    [HttpPost]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> AddNew(
        [FromForm] string model,
        [FromForm] IFormFile? artwork)
    {
        var playlistInsertModel = JsonSerializer.Deserialize<PlaylistInsertModel>(model)
            ?? throw new AppException(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            return Ok(await _service.AddNew(playlistInsertModel, artwork, uid));
        }
        return Forbid();
    }

    [HttpPost("{playlistId}/repost")]
    [AppAuthorize(ERole.USER)]
    public async Task Repost(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Repost(playlistId, uid);
            return;
        }
        throw new AppException(HttpStatusCode.Forbidden, "Bạn không có quyền thực hiện hành động này");
    }

    [HttpPut("{playlistId}/like")]
    [AppAuthorize(ERole.USER, ERole.ADMIN)]
    public async Task Like(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.Like(playlistId, uid);
            return;
        }
        throw new AppException(HttpStatusCode.Forbidden, "Bạn không có quyền thực hiện hành động này");
    }


    [HttpPut("{playlistId}/information")]
    [AppAuthorize(ERole.USER)]
    public async Task UpdateInfomation(
        [FromForm] string model,
        IFormFile? artwork, 
        [FromRoute] int playlistId)
    {
        var playlistUpdateModel = JsonSerializer.Deserialize<PlaylistUpdateModel>(model)
            ?? throw new AppException(HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ");
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.UpdateInfomation(playlistId, playlistUpdateModel, artwork, uid);
            return;
        }
        throw new AppException(HttpStatusCode.Forbidden, "Bạn không có quyền thực hiện hành động này");
    }

    [HttpDelete("user/{playlistId}")]
    [AppAuthorize(ERole.USER)]
    public async Task DeleteByCreator(int playlistId)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long uid))
        {
            await _service.DeleteByCreator(playlistId, uid);
            return;
        }
        throw new AppException(HttpStatusCode.Forbidden, "Bạn không có quyền thực hiện hành động này");
    }

    [HttpDelete("admin/{playlistId}")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task DeleteByAdmin(int playlistId)
    {
        await _service.DeleteByAdmin(playlistId);
    }
}