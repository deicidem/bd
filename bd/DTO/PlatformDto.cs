using System.ComponentModel.DataAnnotations;

namespace bd.DTO;

public class PlatformDto
{
    public string? Id {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public int TotalUsers {get;set;}
    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate {get;set;}
    public IEnumerable<GameDto>? Games { get; set; }
}