using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetAllTrack()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost("add")]
    public async Task Upload([FromForm] TrackInsertModel model)
    {
        var userId = User.FindFirstValue("userid");
        if (userId != null && long.TryParse(userId, out long id))
        {
            await _service.AddNew(model, id);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrack([FromBody] TrackUpdateModel model, int id)
    {
        await _service.UpdateInfomation(model, id);
        return Ok();
    }
}