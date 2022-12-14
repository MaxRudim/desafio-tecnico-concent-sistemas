using System.ComponentModel.DataAnnotations;

namespace ConcentKitchen.Models
{
  public class OrderDish
  {

    [Key]
    public Guid Id { get; set; }

    // Chave estrangeira
    public Guid DishId { get; set; }
    public Guid OrderId { get; set; }


    // // Propriedades de navegação
    public ICollection<Dish>? Dish { get; set; }
    public ICollection<Order>? Order { get; set; }

  }
}