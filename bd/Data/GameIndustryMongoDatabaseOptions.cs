namespace bd.Data;

public class GameIndustryMongoDatabaseOptions
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string GamesCollectionName { get; set; } = null!;
    public string PlatformsCollectionName { get; set; } = null!;
    public string PublishersCollectionName { get; set; } = null!;
}