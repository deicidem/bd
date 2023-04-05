using bd.Services;
using Microsoft.AspNetCore.Mvc;

namespace bd.Controllers;

[ApiController]
[Route("[controller]")]
public class SeedController : Controller
{
    private readonly ILogger<SeedController> _logger;

    private readonly ISeedingService _service;

    public SeedController(ILogger<SeedController> logger, ISeedingService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task Seed()
    {
        await _service.Seed();
    }
}