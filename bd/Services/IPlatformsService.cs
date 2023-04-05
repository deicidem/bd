using bd.DTO;

namespace bd.Services;

public interface IPlatformsService
{
    Task<List<PlatformDto>> GetAsync();
    Task<PlatformDto> GetAsync(string id);
    Task CreateAsync(PlatformDto platform);
    Task UpdateAsync(string id, PlatformDto platform);
    Task RemoveAsync(string id);
}