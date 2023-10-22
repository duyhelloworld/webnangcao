using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webnangcao.Entities;
namespace webnangcao.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;
    public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GetAllAsync()
    {
        await Task.CompletedTask;
        return Ok();
    }
}