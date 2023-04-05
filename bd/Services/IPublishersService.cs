using bd.DTO;

namespace bd.Services;

public interface IPublishersService
{
    Task<List<PublisherDto>> GetAsync();
    Task<PublisherDto> GetAsync(string id);
    Task CreateAsync(PublisherDto publisher);
    Task UpdateAsync(string id, PublisherDto publisher);
    Task RemoveAsync(string id);
}