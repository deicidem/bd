using bd.Data;
using bd.Data.PostgresModels;
using bd.DTO;
using bd.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace bd.Services.PostgresServices;

public class PublishersService: IPublishersService
{
    private readonly GameIndustryPostgresContext _context;
    private readonly PublisherMapper _mapper;

    public PublishersService(GameIndustryPostgresContext context, PublisherMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PublisherDto>> GetAsync()
    {
        return await _context.Publishers
            .Select(p => PublisherMapper.ModelToDto(p, p.Games)).ToListAsync();
    }

    public async Task<PublisherDto> GetAsync(string id)
    {
        var publisher = await _context.Publishers
            .Include(p => p.Games)
            .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        return PublisherMapper.ModelToDto(publisher, publisher.Games);
    }

    public async Task CreateAsync(PublisherDto publisher)
    {
        await _context.Publishers.AddAsync(PublisherMapper.DtoToPostgresModel(publisher));
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(string id, PublisherDto publisher)
    {
        _context.Publishers.Update(PublisherMapper.DtoToPostgresModel(publisher));
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string id)
    {
        var publisher = await _context.Publishers.FindAsync(int.Parse(id));
        if (publisher != null) _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }
}