using bd.Data;
using bd.Data.PostgresModels;
using bd.DTO;
using bd.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace bd.Services.PostgresServices;

public class GamesService: IGamesService
{
    private readonly GameIndustryPostgresContext _context;

    public GamesService(GameIndustryPostgresContext context)
    {
        _context = context;
    }

    public async Task<ICollection<GameDto>> GetAsync()
    {
        var start = DateTime.Now;
        var games = await _context.Games.Include(g => g.Platform).Include(g => g.Publisher).ToListAsync();
        Console.WriteLine($"Time: {DateTime.Now.Ticks - start.Ticks}");
        return games.Select(g => GameMapper.ModelToDto(g, g.Platform, g.Publisher)).ToList();
    }

    public async Task<GameDto> GetAsync(string id)
    {
        var game = await _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Platform)
            .FirstOrDefaultAsync(g => g.Id.ToString() == id);
        return GameMapper.ModelToDto(game, game.Platform, game.Publisher);
    }

    public async Task CreateAsync(GameDto game)
    {
        await _context.Games.AddAsync(GameMapper.DtoToPostgresModel(game));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string id, GameDto game)
    {
        _context.Games.Update(GameMapper.DtoToPostgresModel(game));
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string id)
    {
        var game = await _context.Games.FindAsync(int.Parse(id));
        if (game != null) _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
}