using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bd.Data.PostgresModels;

public class Game
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    [ForeignKey("Platform")]
    public int PlatformId { get; set; }
    public virtual Platform Platform { get; set; }
    
    [ForeignKey("Publisher")]
    public int PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
}