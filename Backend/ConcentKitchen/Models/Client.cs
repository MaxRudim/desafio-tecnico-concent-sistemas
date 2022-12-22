using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcentKitchen.Models
{
  public class Client
  {

    [Key]
    public Guid ClientId { get; set; }

    [Required]
    public int TableNumber { get; set; }

    [StringLength(25, MinimumLength = 2, ErrorMessage = "Invalid name length")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Cpf { get; set; } = string.Empty;

    public ICollection<Order>? Orders { get; set; }

  }

  [NotMapped]
  public class LoginData
  {
    [NotMapped]
    public string Cpf { get; set; } = string.Empty;
    [NotMapped]
    public string Name { get; set; } = string.Empty;
  }
}
