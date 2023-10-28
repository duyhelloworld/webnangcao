using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace webnangcao.Controllers;

[Route("[controller]")]
[ApiController]
public class TrackController : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> SayHello()
    {
        await Task.CompletedTask;
        return Ok("Hello World");
    }

    [HttpGet("upload")]
    public IActionResult Upload(IFormFile file)
    {
        Console.WriteLine(file.FileName);
        return Ok("Uploaded");
    }
}