using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Models.Securities;
using webnangcao.Services;

namespace webnangcao.Controllers;

[ApiController]
// [EnableCors("MyAllowSpecificOrigins")]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetRoot()
    {
        return BadRequest("Truy cập <a href=\"postman.com\">Postman</a> để dùng API");
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupModel model)
    {
        return Ok(await _service.SignUpAsync(model));
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] SigninModel model)
    {
        return Ok(await _service.SignInAsync(model));
    }

    [HttpPost("validate")]
    public async Task<bool> Validate()
    {
        if (Request.Headers.ContainsKey("Authorization"))
        {
            return await _service.ValidateToken(
                Request.Headers["Authorization"]!.First(au => au!.StartsWith("Bearer"))!
                .Replace("Bearer ", ""));     
        }
        return false;
    }

    [HttpGet("signout")]
    public async Task Signout()
    {
        await _service.SignOutAsync();
    }
}