using System.ComponentModel.DataAnnotations;

namespace ConcentKitchen.Models
{
  public class Dish
  {
    [Key]
    public Guid DishId { get; set; }

    [Required]
    public string DishName { get; set; }

    [Required]
    public float DishPrice { get; set; }

    [Required]
    public int DishConclusionInMinutes { get; set; }

    [Required]
    public string DishCategory { get; set; }

    [Required]
    public string DishIngredients { get; set; }

    // public ICollection<Order> Orders { get; set; }
  }
}