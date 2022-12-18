using System.ComponentModel.DataAnnotations;

namespace ConcentKitchen.Models
{
  public class Dish
  {
    [Key]
    public Guid DishId { get; set; }

    [Required]
    public string DishName { get; set; } = string.Empty;

    [Required]
    public float DishPrice { get; set; }

    [Required]
    public int DishPreparationTimeInMinutes { get; set; }

    [Required]
    public string DishCategory { get; set; } = string.Empty;

    [Required]
    public string DishIngredients { get; set; } = string.Empty;

  }
}