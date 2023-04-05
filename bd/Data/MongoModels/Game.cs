using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bd.Data.MongoModels;

public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string PlatformId { get; set; }
    public string PublisherId { get; set; }
}