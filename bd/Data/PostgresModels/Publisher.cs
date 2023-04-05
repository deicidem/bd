using System.ComponentModel.DataAnnotations;

namespace bd.Data.PostgresModels;

public class Publisher
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Budget { get; set; }
    public DateTime FoundationDate { get; set; }
    
    public virtual ICollection<Game> Games { get; set; }
}