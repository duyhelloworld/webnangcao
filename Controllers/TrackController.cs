using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> SayHello()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("upload")]
    public async Task Upload(IFormFile file)
    {
        Console.WriteLine(file.FileName);
        await _service.UploadTrack(file);
        Console.WriteLine("Upload success");
    }
}