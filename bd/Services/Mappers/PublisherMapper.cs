using bd.DTO;
using MongoPublisher = bd.Data.MongoModels.Publisher;
using PostgresPublisher = bd.Data.PostgresModels.Publisher;
using PostgresGame = bd.Data.PostgresModels.Game;
namespace bd.Services.Mappers;

public class PublisherMapper
{
    public static PublisherDto ModelToDto(PostgresPublisher? model)
    {
        return new PublisherDto()
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Budget = model.Budget,
            FoundationDate = model.FoundationDate,
        };
    }
    public static PublisherDto ModelToDto(PostgresPublisher? model, IEnumerable<PostgresGame> games)
    {
        return new PublisherDto()
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Budget = model.Budget,
            FoundationDate = model.FoundationDate,
            Games = games.Select(GameMapper.ModelToDto).ToList()
        };
    }
    public static PublisherDto ModelToDto(MongoPublisher model)
    {
        return new PublisherDto()
        {
            Id = model.Id,
            Name = model.Name,
            Budget = model.Budget,
            FoundationDate = model.FoundationDate,
        };
    }
    
    public static PostgresPublisher DtoToPostgresModel(PublisherDto dto)
    {
        return new PostgresPublisher()
        {
            Id = int.Parse(dto.Id ?? string.Empty),
            Name = dto.Name,
            Budget = dto.Budget,
            FoundationDate = dto.FoundationDate,
        };
    }
    public static MongoPublisher DtoToMongoModel(PublisherDto dto)
    {
        return new MongoPublisher()
        {
            Id = dto?.Id,
            Name = dto.Name,
            Budget = dto.Budget,
            FoundationDate = dto.FoundationDate,
        };
    }
}