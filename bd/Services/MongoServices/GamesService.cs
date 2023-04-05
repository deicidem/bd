using bd.Data;
using bd.DTO;
using bd.Services.Mappers;
using bd.Data.MongoModels;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Platform = bd.Data.PostgresModels.Platform;

namespace bd.Services.MongoServices;

public class GamesService : IGamesService
{
    private readonly GameIndustryMongoContext _context;
    private readonly GameMapper _mapper;

    public GamesService(GameIndustryMongoContext context, GameMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<GameDto>> GetAsync()
    {
        var start = DateTime.Now;
        var games = await _context.Games().Find(_ => true).ToListAsync();
        var gamesFull = games.Select(g => new
        {
            Game = g,
            Platform = _context.Platforms().Find(a => a.Id == g.PlatformId).FirstOrDefault(),
            Publisher = _context.Publishers().Find(a => a.Id == g.PublisherId).FirstOrDefault()
        });
        Console.WriteLine($"Time: {DateTime.Now.Ticks - start.Ticks}");
        return gamesFull.Select(g => GameMapper.ModelToDto(g.Game, g.Platform, g.Publisher)).ToList();
    }

    public async Task<GameDto> GetAsync(string id)
    {
        var game = await _context.Games().Find(g => g.Id.ToString() == id).FirstOrDefaultAsync();
        var platform =  _context.Platforms().Find(a => a.Id == game.PlatformId).FirstOrDefault();
        var publisher =  _context.Publishers().Find(a => a.Id == game.PublisherId).FirstOrDefault();
        return GameMapper.ModelToDto(game, platform, publisher);
    }

    public async Task CreateAsync(GameDto game)
    {
        await _context.Games().InsertOneAsync(GameMapper.DtoToMongoModel(game));
    }

    public async Task UpdateAsync(string id, GameDto game)
    {
        await _context.Games().ReplaceOneAsync(g => g.Id == id, GameMapper.DtoToMongoModel(game));
    }

    public async Task RemoveAsync(string id)
    {
        await _context.Games().DeleteOneAsync(g => g.Id == id);
    }
}
