using System.ComponentModel.DataAnnotations;

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
    public string Name { get; set; }

    [Required]
    public string Cpf { get; set; }

    public ICollection<Order>? Orders { get; set; }

  }
}
