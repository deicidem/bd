using bd.Data;
using bd.Data.PostgresModels;
using Bogus;

namespace bd.Services.PostgresServices;

public class SeedingService : ISeedingService
{
    private readonly GameIndustryPostgresContext _context;
    private const int N = 100000;
    public SeedingService(GameIndustryPostgresContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        _context.Games.RemoveRange(_context.Games);
        _context.Platforms.RemoveRange(_context.Platforms);
        _context.Publishers.RemoveRange(_context.Publishers);

        var start = DateTime.Now;
        var testPlatforms = new Faker<Platform>()
            .RuleFor(p => p.Name, f => $"Platform {f.IndexFaker + 1}")
            .RuleFor(p => p.TotalUsers, f => f.Random.Number(1, Int32.MaxValue))
            .RuleFor(p => p.ReleaseDate, f => f.Date.Between(new DateTime(2010, 1, 1), DateTime.Now).ToUniversalTime());

        var platforms = testPlatforms.Generate(N).ToArray();
        await _context.Platforms.AddRangeAsync(platforms);
        await _context.SaveChangesAsync();
        
        var testPublishers = new Faker<Publisher>()
            .RuleFor(p => p.Name, f => $"Publisher {f.IndexFaker + 1}")
            .RuleFor(p => p.Budget, f => f.Random.Number(10000, Int32.MaxValue))
            .RuleFor(p => p.FoundationDate, f => f.Date.Between(new DateTime(2010, 1, 1), DateTime.Now).ToUniversalTime());

        var publishers = testPublishers.Generate(N).ToArray();
        await _context.Publishers.AddRangeAsync(publishers);
        await _context.SaveChangesAsync();
        
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
        await _context.Games.AddRangeAsync(games);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Seeding Time: {DateTime.Now.Ticks - start.Ticks}");
    }
}