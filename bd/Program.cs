using bd.Data;
using bd.Services;
using bd.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MongoServices = bd.Services.MongoServices;
using PostgresServices = bd.Services.PostgresServices;

var builder = WebApplication.CreateBuilder(args);

var databaseType = builder.Configuration["DatabaseType"];
// Add services to the container.
if (databaseType == "postgres")
{
    builder.Services.AddDbContext<GameIndustryPostgresContext>(
        options => options
            .UseNpgsql(builder.Configuration["PostgresDatabase:ConnectionString"]));
    builder.Services.AddScoped<ISeedingService, PostgresServices.SeedingService>();
    builder.Services.AddScoped<IGamesService, PostgresServices.GamesService>();
    builder.Services.AddScoped<IPlatformsService, PostgresServices.PlatformsService>();
    builder.Services.AddScoped<IPublishersService, PostgresServices.PublishersService>();
} else if (databaseType == "mongo")
{
    builder.Services.Configure<GameIndustryMongoDatabaseOptions>(builder.Configuration.GetSection("MongoDatabase"));
    builder.Services.AddScoped<GameIndustryMongoContext>();
    builder.Services.AddScoped<ISeedingService, MongoServices.SeedingService>();
    builder.Services.AddScoped<IGamesService, MongoServices.GamesService>();
    builder.Services.AddScoped<IPlatformsService, MongoServices.PlatformsService>();
    builder.Services.AddScoped<IPublishersService, MongoServices.PublishersService>();
}

builder.Services.AddScoped<GameMapper>();
builder.Services.AddScoped<PublisherMapper>();
builder.Services.AddScoped<PlatformMapper>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();