using System.ComponentModel.DataAnnotations;

namespace ConcentKitchen.Models
{
  public class Order
  {

    [Key]
    public Guid OrderId { get; set; }
    public int TotalPrice { get; set; }
    public string Status { get; set; }
    public DateTime OrderTime { get; set; }
    public DateTime CompletionDeadline { get; set; }

    // Chave estrangeira
    public Guid ClientId { get; set; }

    // // Propriedades de navegação
    public Client? Client { get; set; }

  }
}