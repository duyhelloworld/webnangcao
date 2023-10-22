using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace webnangcao.Controllers;

[Route("[controller]")]
[ApiController]
public class TrackController : ControllerBase
{
    [HttpGet("/")]
    [Authorize(AuthenticationSchemes = FacebookDefaults.AuthenticationScheme)]
    public async Task<IActionResult> SayHello()
    {
        await Task.CompletedTask;
        return Ok("Hello World");
    }
}