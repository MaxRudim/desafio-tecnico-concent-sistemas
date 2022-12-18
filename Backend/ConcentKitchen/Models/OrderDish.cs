using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcentKitchen.Models
{
  public class OrderDish
  {

    [Key]
    public Guid Id { get; set; }

    // Chave estrangeira
    [ForeignKey("DishId")]
    public Guid DishId { get; set; }
    [ForeignKey("OrderId")]
    public Guid OrderId { get; set; }
    // public Guid ClientId { get; set; }


    // // Propriedades de navegação
    public ICollection<Dish>? Dish { get; set; }
    public ICollection<Order>? Order { get; set; }

  }
}