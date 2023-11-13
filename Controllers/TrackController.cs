using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Enumerables;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Securities;
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
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetAllTrack()
    {
        return Ok(await _service.GetAll());
    }
    [HttpGet("user/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetTrackByUserId(int id)
    {
        return Ok(await _service.GetByUserId(id));
    }
    [HttpGet("name/{name}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetTrackByName(string name)
    {
        return Ok(await _service.GetByName(name));
    }
    
    [HttpPost("add")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Upload([FromForm] TrackInsertModel model)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id))
        {
            await _service.UploadTrack(model, id);
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("update/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Update([FromForm] TrackUpdateModel model, int id)
    {
        await _service.UpdateInfomation(model, id);
        return Ok();
    }
    [HttpDelete("delete/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remove(id);
        return Ok();
    }
    [HttpPost("like/{id}")]
    [AppAuthorize(ERole.USER)]
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
    [HttpPost("comment/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Comment(int id, [FromBody] CommentInsertModel model)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && int.TryParse(userId, out int uid))
        {
            await _service.CommentTrack(id, uid, model.Content);
            return Ok();
        }
        return BadRequest();
    }
    [HttpGet("comment/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetComment(int id)
    {
        return Ok(await _service.GetComment(id));
    }
    [HttpPut("comment/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> EditComment(int id, [FromBody] CommentUpdateModel model)
    {
        await _service.EditComment(id, model.Content);
        return Ok();
    }
}