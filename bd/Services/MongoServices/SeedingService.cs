using bd.Data;
using bd.Data.MongoModels;
using Bogus;
using MongoDB.Driver;

namespace bd.Services.MongoServices;

public class SeedingService : ISeedingService
{
    private readonly GameIndustryMongoContext _context;
    private const int N = 100000;
    public SeedingService(GameIndustryMongoContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        await _context.Games().DeleteManyAsync(_ => true);
        await _context.Platforms().DeleteManyAsync(_ => true);
        await _context.Publishers().DeleteManyAsync(_ => true);

        var start = DateTime.Now;
        var testPlatforms = new Faker<Platform>()
            .RuleFor(p => p.Name, f => $"Platform {f.IndexFaker + 1}")
            .RuleFor(p => p.TotalUsers, f => f.Random.Number(1, Int32.MaxValue))
            .RuleFor(p => p.ReleaseDate, f => f.Date.Between(new DateTime(2010, 1, 1), DateTime.Now).ToUniversalTime());

        var platforms = testPlatforms.Generate(N).ToArray();
        await _context.Platforms().InsertManyAsync(platforms);

        var testPublishers = new Faker<Publisher>()
            .RuleFor(p => p.Name, f => $"Publisher {f.IndexFaker + 1}")
            .RuleFor(p => p.Budget, f => f.Random.Number(10000, Int32.MaxValue))
            .RuleFor(p => p.FoundationDate, f => f.Date.Between(new DateTime(2010, 1, 1), DateTime.Now).ToUniversalTime());
        
        var publishers = testPublishers.Generate(N).ToArray();
        await _context.Publishers().InsertManyAsync(publishers);

        var testGames = new Faker<Game>()
            .RuleFor(g => g.Title, f => $"Game {f.IndexFaker + 1}")
            .RuleFor(g => g.Description, f => f.Lorem.Paragraph())
            .RuleFor(g => g.PlatformId, f => f.PickRandom(platforms).Id)
            .RuleFor(g => g.PublisherId, f => f.PickRandom(publishers).Id)
            .RuleFor(g => g.ReleaseDate, (f, g) =>
            {
                var platform = platforms.First(p => p.Id == g.PlatformId);
                var publisher = publishers.First(p => p.Id == g.PublisherId);
                return platform.ReleaseDate > publisher.FoundationDate
                    ? f.Date.Between(platform.ReleaseDate, DateTime.Now)
                    : f.Date.Between(publisher.FoundationDate, DateTime.Now);
            });

        var games = testGames.Generate(N).ToArray();
        await _context.Games().InsertManyAsync(games);
        Console.WriteLine($"Seeding Time: {DateTime.Now.Ticks - start.Ticks}");
    }
}