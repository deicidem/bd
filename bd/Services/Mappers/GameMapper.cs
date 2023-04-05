using bd.DTO;
using MongoGame = bd.Data.MongoModels.Game;
using MongoPlatform = bd.Data.MongoModels.Platform;
using MongoPublisher = bd.Data.MongoModels.Publisher;
using PostgresGame = bd.Data.PostgresModels.Game;
using PostgresPlatform = bd.Data.PostgresModels.Platform;
using PostgresPublisher = bd.Data.PostgresModels.Publisher;
namespace bd.Services.Mappers;

public class GameMapper
{
    
    public static GameDto ModelToDto(PostgresGame model)
    {
        return new GameDto()
        {
            Id = model.Id.ToString(),
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            Title = model.Title,
        };
    }
    public static GameDto ModelToDto(PostgresGame model, PostgresPlatform platform, PostgresPublisher publisher)
    {
        return new GameDto()
        {
            Id = model.Id.ToString(),
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            Title = model.Title,
            Publisher = PublisherMapper.ModelToDto(publisher),
            Platform = PlatformMapper.ModelToDto(platform),
        };
    }
    
    public static GameDto ModelToDto(MongoGame model)
    {
        return new GameDto()
        {
            Id = model.Id,
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            Title = model.Title
        };
    }
    
    public static GameDto ModelToDto(MongoGame model, MongoPlatform platform, MongoPublisher publisher)
    {
        return new GameDto()
        {
            Id = model.Id,
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            Title = model.Title,
            Publisher = PublisherMapper.ModelToDto(publisher),
            Platform = PlatformMapper.ModelToDto(platform),
        };
    }
    
    public static PostgresGame DtoToPostgresModel(GameDto dto)
    {
        return new PostgresGame()
        {
            Id = int.Parse(dto.Id ?? string.Empty),
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            Title = dto.Title
        };
    }
    public static MongoGame DtoToMongoModel(GameDto dto)
    {
        return new MongoGame()
        {
            Id = dto?.Id,
            Description = dto.Description,
            ReleaseDate = dto.ReleaseDate,
            Title = dto.Title
        };
    }
}