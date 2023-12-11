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
    public async Task<IActionResult> UpdateCommentByCreator(int id, long userId, CommentUpdateModel model)
    {
        System.Console.WriteLine("User id: " + userId);
        await _service.UpdateCommentByCreator(id, userId, model);
        return Ok();
    }
    [HttpPut("report/{id}")]
    public async Task<IActionResult> ReportComment(int id, long userId)
    {
        await _service.ReportComment(id, userId);
        return Ok();
    }

    [HttpDelete("{id}")]
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> DeleteCommentByCreator(int id)
    {
        var role = User.FindFirstValue("role");
        if(role == "ADMIN")
        {
            await _service.DeleteCommentByAdmin(id);
            return Ok();
        }
        else if(role == "User")
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
    // [AppAuthorize(ERole.USER)]
    public async Task<IActionResult> Comment([FromBody] CommentInsertModel model, long userId, int id)
    {
        await _service.Comment(model, userId, id);
        return Ok();
    }
}