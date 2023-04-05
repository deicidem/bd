using bd.Data;
using bd.Data.MongoModels;
using bd.DTO;
using bd.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace bd.Services.MongoServices;

public class PublishersService: IPublishersService
{
    private readonly GameIndustryMongoContext _context;
    private readonly PublisherMapper _mapper;

    public PublishersService(GameIndustryMongoContext context, PublisherMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PublisherDto>> GetAsync()
    {
        var list = await _context.Publishers().Find( _ =>  true).ToListAsync();
        return list.Select(p => PublisherMapper.ModelToDto(p)).ToList();
    }

    public async Task<PublisherDto> GetAsync(string id)
    {
        return  PublisherMapper.ModelToDto(await _context.Publishers().Find(p => p.Id.ToString() == id).FirstOrDefaultAsync());
    }

    public async Task CreateAsync(PublisherDto publisher)
    {
        await _context.Publishers().InsertOneAsync(PublisherMapper.DtoToMongoModel(publisher));
    }

    public async Task UpdateAsync(string id, PublisherDto publisher)
    {
        await _context.Publishers().ReplaceOneAsync(p => p.Id == id , PublisherMapper.DtoToMongoModel(publisher));
    }

    public async Task RemoveAsync(string id)
    {
        await _context.Platforms().DeleteOneAsync(p => p.Id == id);
    }
}