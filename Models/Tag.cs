using System.ComponentModel.DataAnnotations;

namespace keepr_angular.Models
{
  public class Tag
  {
    public int Id { get; set; }
    [Required]
    [MinLength(4)]
    public string TagName { get; set; }
    public string AuthorId { get; set; }
    public int KeepId { get; set; }
  }
}