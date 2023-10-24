using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Entities.Enumerables;
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

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignupModel model)
    {
        return Ok(await _service.SignUpAsync(model, "User"));
    }

    [Authorize(Roles = nameof(ERole.Admin) + "," + nameof(ERole.SuperAdmin))]
    [HttpPost("signup/{role:minlength(1)}")]
    public async Task<IActionResult> SignUp([FromBody] SignupModel model, [FromRoute] string role)
    {
        return Ok(await _service.SignUpAsync(model, role));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        return Ok(await _service.SignInAsync(model));
    }
}