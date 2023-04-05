using System.ComponentModel.DataAnnotations;

namespace bd.DTO;

public class PublisherDto
{
    public string? Id {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public int Budget {get;set;}
    [Required]
    [DataType(DataType.Date)]
    public DateTime FoundationDate {get;set;}
    public IEnumerable<GameDto>? Games { get; set; }
}