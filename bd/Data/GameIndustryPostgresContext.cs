using bd.Data.PostgresModels;
using Microsoft.EntityFrameworkCore;

namespace bd.Data;

public class GameIndustryPostgresContext : DbContext
{
    protected GameIndustryPostgresContext()
    {
    }

    public GameIndustryPostgresContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<Platform> Platforms { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
}