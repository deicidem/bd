using bd.Data.MongoModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bd.Data;

public class GameIndustryMongoContext
{
    private IMongoDatabase _db { get; set; }
    private IMongoClient _client { get; set; }
    private readonly IMongoCollection<Game> _gamesCollection;
    private readonly IMongoCollection<Platform> _platformCollection;
    private readonly IMongoCollection<Publisher> _publisherCollection;

    public GameIndustryMongoContext(IOptions<GameIndustryMongoDatabaseOptions> options)
    {
        _client = new MongoClient(options.Value.ConnectionString);
        _db = _client.GetDatabase(options.Value.DatabaseName);
        _gamesCollection = _db.GetCollection<Game>(options.Value.GamesCollectionName);
        _platformCollection = _db.GetCollection<Platform>(options.Value.PlatformsCollectionName);
        _publisherCollection = _db.GetCollection<Publisher>(options.Value.PublishersCollectionName);
    }

    public IMongoCollection<Game> Games() => _gamesCollection;
    public IMongoCollection<Platform> Platforms() => _platformCollection;
    public IMongoCollection<Publisher> Publishers() => _publisherCollection;
}