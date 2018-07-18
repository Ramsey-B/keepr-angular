using System.ComponentModel.DataAnnotations;

namespace keepr_angular.Models
{
  public class Keep 
  {
    public int Id { get; set; }
    public string Img { get; set; }
    [Required]
    [MinLength(4)]
    public string Description { get; set; }
    public string Author { get; set; }
    public string AuthorId { get; set; }
    public int Views { get; set; } = 0;
    public int Keeps { get; set; } = 0;
    public bool Public { get; set; } = false;
  }
}