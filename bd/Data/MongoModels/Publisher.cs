using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bd.Data.MongoModels;

public class Publisher
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public int Budget { get; set; }
    public DateTime FoundationDate { get; set; }
}