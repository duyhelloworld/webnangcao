using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Entities.Enumerables;
using webnangcao.Models;
using webnangcao.Models.Inserts;
using webnangcao.Models.Securities;
using webnangcao.Models.Updates;
using webnangcao.Services;
namespace webnangcao.Controllers;

[ApiController]
[Route("[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackService _service;
    private readonly IConfiguration _config;
    public TrackController(ITrackService service, IConfiguration config)
    {
        _service = service;
        _config = config;
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