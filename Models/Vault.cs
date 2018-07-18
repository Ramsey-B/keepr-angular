using System.ComponentModel.DataAnnotations;

namespace keepr_angular.Models
{
  public class Vault
  {
    public int Id { get; set; }
    [Required]
    [MinLength(4)]
    public string Title { get; set; }
    [MinLength(4)]
    public string AuthorId { get; set; }

  }
}