using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webnangcao.Entities;
using webnangcao.Models.Securities;
using webnangcao.Services;
namespace webnangcao.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignupModel model)
    {
        return Ok(await _service.Signup(model));
    }

    [HttpPost("login")]
    public async Task Login()
    {
        await _service.SigninAsync(context);
    }

    [HttpGet]
    public IActionResult SayHello()
    {
        return Ok("Hello World");
    }
}