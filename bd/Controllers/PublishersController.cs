using bd.DTO;
using bd.Services;
using Microsoft.AspNetCore.Mvc;

namespace bd.Controllers;

[ApiController]
[Route("[controller]")]
public class PublishersController : Controller
{
    private readonly ILogger<PublishersController> _logger;

    private readonly IPublishersService _service;

    public PublishersController(ILogger<PublishersController> logger, IPublishersService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ICollection<PublisherDto>> Get()
    {
        return await _service.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<PublisherDto?> GetOne(string id) => await _service.GetAsync(id);
    
    [HttpPost]
    public async Task<IActionResult> Post(PublisherDto publisher)
    {
        await _service.CreateAsync(publisher);

        return CreatedAtAction(nameof(Get), new { id = publisher.Id }, publisher);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, PublisherDto updatedPublisher)
    {
        var publisher = await _service.GetAsync(id);
        
        if (publisher is null) return NotFound();

        updatedPublisher.Id = publisher.Id;
        await _service.UpdateAsync(id, updatedPublisher);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var publisher = await _service.GetAsync(id);

        if (publisher is null) return NotFound();

        await _service.RemoveAsync(id);

        return NoContent();
    }
}