using bd.DTO;

namespace bd.Services;

public interface IGamesService
{
    Task<ICollection<GameDto>> GetAsync();
    Task<GameDto> GetAsync(string id);
    Task CreateAsync(GameDto game);
    Task UpdateAsync(string id, GameDto game);
    Task RemoveAsync(string id);
}