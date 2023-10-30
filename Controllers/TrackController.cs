using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Entities;
using webnangcao.Entities.Enumerables;
using webnangcao.Models;
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
    // [Authorize(Roles = nameof(ERole.ADMIN))]
    public async Task<IActionResult> GetAllTrack()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost("add")]
    public async Task Upload([FromForm] TrackInsertModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            await _service.AddNew(model, userId);
            Console.WriteLine("OK");
        }
    }

    [HttpPost("upload")]
    public async Task UploadFile(IFormFile file)
    {
        await _service.UploadCache(file);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrack([FromBody] TrackUpdateModel model, int id)
    {
        await _service.UpdateInfomation(model, id);
        return Ok();
    }
}