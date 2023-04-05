using System.ComponentModel.DataAnnotations;

namespace bd.Data.PostgresModels;

public class Platform
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int TotalUsers { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public virtual ICollection<Game> Games { get; set; }
}