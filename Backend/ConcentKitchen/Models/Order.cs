using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConcentKitchen.Models
{
  public class Order
  {

    [Key]
    public Guid OrderId { get; set; } = new Guid();

    [Required]
    public float TotalPrice { get; set; } = 0;

    [Required]
    public string Status { get; set; } = string.Empty;

    public DateTime OrderTime { get; set; } = DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR"));
    public DateTime CompletionDeadline { get; set; } = DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR"));

    // Chave estrangeira
    public Guid ClientId { get; set; }

    // // Propriedades de navegação
    public Client? Client { get; set; }

  }
}