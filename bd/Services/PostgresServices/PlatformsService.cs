using bd.Data;
using bd.Data.PostgresModels;
using bd.DTO;
using bd.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace bd.Services.PostgresServices;

public class PlatformsService: IPlatformsService
{
    private readonly GameIndustryPostgresContext _context;
    private readonly PlatformMapper _mapper;

    public PlatformsService(GameIndustryPostgresContext context, PlatformMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PlatformDto>> GetAsync()
    {
        var start = DateTime.Now;
        var platforms = await _context.Platforms
            .Select(p => PlatformMapper.ModelToDto(p, p.Games))
            .ToListAsync();
        Console.WriteLine($"Time: {DateTime.Now.Ticks - start.Ticks}");
        return platforms;
    }

    public async Task<PlatformDto> GetAsync(string id)
    {
        var platform = await _context.Platforms
            .Include(p => p.Games)
            .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        return PlatformMapper.ModelToDto(platform, platform.Games);
    }

    public async Task CreateAsync(PlatformDto platform)
    {
        await _context.Platforms.AddAsync(PlatformMapper.DtoToPostgresModel(platform));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string id, PlatformDto platform)
    {
        _context.Platforms.Update(PlatformMapper.DtoToPostgresModel(platform));
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string id)
    {
        var platform = await _context.Platforms.FindAsync(int.Parse(id));
        if (platform != null) _context.Platforms.Remove(platform);
        await _context.SaveChangesAsync();
    }
}