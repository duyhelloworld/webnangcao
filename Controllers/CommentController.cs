using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using webnangcao.Entities;
using webnangcao.Enumerables;
using webnangcao.Models.Inserts;
using webnangcao.Models.Updates;
using webnangcao.Services;
using webnangcao.Tools;

namespace webnangcao.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;
    public CommentController(ICommentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllComment()
    {
        return Ok(await _service.GetAll());
    }
    [HttpGet("violation")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetViolationComment()
    {
        return Ok(await _service.GetViolationComment());
    }
    [HttpGet("track/{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> GetCommentByTrackId(int id)
    {
        return Ok(await _service.GetByTrackId(id));
    }

    [HttpPut("{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> UpdateCommentByCreator(int id, CommentUpdateModel model)
    {
        var userIdString = User.FindFirstValue("userid");
        if (long.TryParse(userIdString, out var userId))
        {
            await _service.UpdateCommentByCreator(id, userId, model);
            return Ok();
        }
        else
        {
            return BadRequest("Invalid user id.");
        }
    }
    [HttpPut("report/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> ReportComment(int id)
    {
        var userIdString = User.FindFirstValue("userid");
        if (long.TryParse(userIdString, out var userId))
        {
            await _service.ReportComment(id, userId);
            return Ok();
        }
        else
        {
            return BadRequest("Invalid user id.");
        }
    }
    [HttpPut("unreport/{id}")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> UnReportComment(int id)
    {
        await _service.UnReportComment(id);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    [AppAuthorize(ERole.USER, ERole.ADMIN)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var role = User.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        if (role == "ADMIN")
        {
            await _service.DeleteCommentByAdmin(id);
            return Ok();
        }
        else if (role == "USER")
        {
            var userid = long.Parse(User.FindFirstValue("userid")!);
            await _service.DeleteCommentByCreator(id, userid);
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }
    [HttpPost("track/{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Comment([FromBody] CommentInsertModel model, int id)
    {
        var userIdString = User.FindFirstValue("userid");
        if (long.TryParse(userIdString, out var userId))
        {
            await _service.Comment(model, userId, id);
            return Ok();
        }
        else
        {
            return BadRequest("Invalid user id.");
        }
    }
}