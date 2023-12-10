using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Enumerables;
using webnangcao.Exceptions;
using webnangcao.Models.Updates;
using webnangcao.Services;
using webnangcao.Tools;

namespace webnangcao.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> GetAll([FromQuery] int page)
    {
        return Ok(await _userService.GetAll(page));
    }

    [HttpGet("{id}")]
    [AppAuthorize(ERole.ADMIN)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpDelete]
    [AppAuthorize(ERole.ADMIN, ERole.USER)]
    public async Task Disable()
    {
        var userid = User.FindFirstValue("userid");
        if (userid != null && long.TryParse(userid, out long uid))
        {
            await _userService.Disable(uid);
        }
        throw new AppException(HttpStatusCode.Forbidden, 
            "Không tìm thấy user");
    }

    [HttpPut("{id}")]
    [AppAuthorize(ERole.USER)]
    public async Task UpdateInformation(
        [FromForm] string model,
        [FromForm] IFormFile? avatar)
    {
        var updateModel = JsonSerializer.Deserialize<UserUpdateModel>(model);
        var userid = User.FindFirstValue("userid");
        if (updateModel == null)
        {
            throw new AppException(HttpStatusCode.BadRequest, 
                "Dữ liệu không hợp lệ");
        }
        if (userid != null && long.TryParse(userid, out long uid))
        {
            await _userService.Update(uid, updateModel, avatar);
            return;
        }
        throw new AppException(HttpStatusCode.Forbidden, 
            "Không tìm thấy user");
    }
}