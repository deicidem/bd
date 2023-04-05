using bd.DTO;
using MongoPlatform = bd.Data.MongoModels.Platform;
using MongoGame = bd.Data.MongoModels.Game;
using PostgresGame = bd.Data.PostgresModels.Game;
using PostgresPlatform = bd.Data.PostgresModels.Platform;
namespace bd.Services.Mappers;

public class PlatformMapper
{
    public static PlatformDto ModelToDto(PostgresPlatform? model)
    {
        return new PlatformDto()
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            TotalUsers = model.TotalUsers,
            ReleaseDate = model.ReleaseDate,
        };
    }
    
    public static PlatformDto ModelToDto(PostgresPlatform? model, IEnumerable<PostgresGame> games)
    {
        return new PlatformDto()
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            TotalUsers = model.TotalUsers,
            ReleaseDate = model.ReleaseDate,
            Games = games.Select(GameMapper.ModelToDto)
        };
    }
    public static PlatformDto ModelToDto(MongoPlatform model)
    {
        return new PlatformDto()
        {
            Id = model.Id,
            Name = model.Name,
            TotalUsers = model.TotalUsers,
            ReleaseDate = model.ReleaseDate,
        };
    }
    public static PlatformDto ModelToDto(MongoPlatform model, IEnumerable<MongoGame> games)
    {
        return new PlatformDto()
        {
            Id = model.Id,
            Name = model.Name,
            TotalUsers = model.TotalUsers,
            ReleaseDate = model.ReleaseDate,
            Games = games.Select(GameMapper.ModelToDto)
        };
    }
    
    public static PostgresPlatform DtoToPostgresModel(PlatformDto dto)
    {
        return new PostgresPlatform()
        {
            Id = int.Parse(dto.Id ?? string.Empty),
            Name = dto.Name,
            TotalUsers = dto.TotalUsers,
            ReleaseDate = dto.ReleaseDate,
        };
    }
    public static MongoPlatform DtoToMongoModel(PlatformDto dto)
    {
        return new MongoPlatform()
        {
            Id = dto?.Id,
            Name = dto.Name,
            TotalUsers = dto.TotalUsers,
            ReleaseDate = dto.ReleaseDate,
        };
    }
}