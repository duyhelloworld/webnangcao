using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Entities;
using webnangcao.Entities.Enumerables;
using webnangcao.Models.Securities;
using webnangcao.Services;

namespace webnangcao.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return BadRequest("Truy cập <a href=\"postman.com\">Postman</a> để dùng POST");
    }

    [AllowAnonymous]
    [HttpPost("signup/user")]
    public async Task<IActionResult> SignUpUser([FromBody] SignupModel model)
    {
        return Ok(await _service.SignUpAsync(model, ERole.USER));
    }

    [Authorize(Roles = nameof(ERole.SUPERADMIN))]
    [HttpPost("signup/admin")]
    public async Task<IActionResult> SignUpAmin([FromBody] SignupModel model)
    {
        return Ok(await _service.SignUpAsync(model, ERole.ADMIN));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        return Ok(await _service.SignInAsync(model));
    }

    // [HttpGet("hello")]
    // [Authorize(Roles = "Admin")]
    // public IActionResult Hello()
    // {
    //     return Ok("Hello");
    // }

    // [HttpGet("hi")] 
    // [Authorize(Roles = nameof(ERole.SUPERADMIN))]
    // public IActionResult Hi()
    // {
    //     return Ok("Hi");
    // }
}