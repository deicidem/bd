using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using bd.Data.PostgresModels;
using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.Json.LitJson;

namespace bd.DTO;

public class GameDto
{
    public string? Id {get;set;}
    [Required]
    public string Title {get;set;}
    [Required]
    public string Description {get;set;}
    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate {get;set;}
    public PlatformDto? Platform { get; set; }
    public PublisherDto? Publisher { get; set; }
}