using bd.DTO;
using bd.Services;
using Microsoft.AspNetCore.Mvc;

namespace bd.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : Controller
{
    private readonly ILogger<GamesController> _logger;

    private readonly IGamesService _service;

    public GamesController(ILogger<GamesController> logger, IGamesService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ICollection<GameDto>> Get()
    {
        return await _service.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<GameDto?> GetOne(string id) => await _service.GetAsync(id);
    
    [HttpPost]
    public async Task<IActionResult> Post(GameDto game)
    {
        await _service.CreateAsync(game);

        return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, GameDto updatedGame)
    {
        var game = await _service.GetAsync(id);
        
        if (game is null) return NotFound();

        updatedGame.Id = game.Id;
        await _service.UpdateAsync(id, updatedGame);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var game = await _service.GetAsync(id);

        if (game is null) return NotFound();

        await _service.RemoveAsync(id);

        return NoContent();
    }
}