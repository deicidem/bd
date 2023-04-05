using bd.Data;
using bd.DTO;
using bd.Services.Mappers;
using MongoDB.Driver;

namespace bd.Services.MongoServices;

public class PlatformsService: IPlatformsService
{
    private readonly GameIndustryMongoContext _context;
    private readonly PlatformMapper _mapper;

    public PlatformsService(GameIndustryMongoContext context, PlatformMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PlatformDto>> GetAsync()
    {
        var list = await _context.Platforms().Find( _ =>  true).ToListAsync();
        return list.Select(PlatformMapper.ModelToDto).ToList();
    }

    public async Task<PlatformDto> GetAsync(string id)
    {
        var platform = await _context.Platforms().Find(p => p.Id.ToString() == id).FirstOrDefaultAsync();
        var games = await _context.Games().Find(g => g.PlatformId == platform.Id).ToListAsync();
        return  PlatformMapper.ModelToDto(platform, games);
    }

    public async Task CreateAsync(PlatformDto platform)
    {
        await _context.Platforms().InsertOneAsync(PlatformMapper.DtoToMongoModel(platform));
    }

    public async Task UpdateAsync(string id, PlatformDto platform)
    {
        await _context.Platforms().ReplaceOneAsync(p => p.Id == id , PlatformMapper.DtoToMongoModel(platform));
    }

    public async Task RemoveAsync(string id)
    {
        await _context.Platforms().DeleteOneAsync(p => p.Id == id);
    }
}