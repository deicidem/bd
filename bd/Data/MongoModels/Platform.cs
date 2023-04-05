using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bd.Data.MongoModels;

public class Platform
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public int TotalUsers { get; set; }
    public DateTime ReleaseDate { get; set; }
}