using bd.DTO;
using bd.Services;
using Microsoft.AspNetCore.Mvc;

namespace bd.Controllers;

[ApiController]
[Route("[controller]")]
public class PlatformsController : Controller
{
    private readonly ILogger<PlatformsController> _logger;

    private readonly IPlatformsService _service;

    public PlatformsController(ILogger<PlatformsController> logger, IPlatformsService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ICollection<PlatformDto>> Get()
    {
        return await _service.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<PlatformDto?> GetOne(string id) => await _service.GetAsync(id);
    
    [HttpPost]
    public async Task<IActionResult> Post(PlatformDto platform)
    {
        await _service.CreateAsync(platform);

        return CreatedAtAction(nameof(Get), new { id = platform.Id }, platform);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, PlatformDto updatedPlatform)
    {
        var platform = await _service.GetAsync(id);
        
        if (platform is null) return NotFound();

        updatedPlatform.Id = platform.Id;
        await _service.UpdateAsync(id, updatedPlatform);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var platform = await _service.GetAsync(id);

        if (platform is null) return NotFound();

        await _service.RemoveAsync(id);

        return NoContent();
    }
}