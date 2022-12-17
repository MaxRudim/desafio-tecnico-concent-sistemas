using ConcentKitchen.Models;

namespace ConcentKitchen.Services;

public interface IDishService
{
    public Task<Dish> CreateDish(Dish dish);
    public Task DeleteDish(Guid id);
    public Task<Dish> UpdateDish(Dish dish);
    public Task<IEnumerable<Dish>> GetAllDishes();
    public Task<Dish> GetDish(string id);

}