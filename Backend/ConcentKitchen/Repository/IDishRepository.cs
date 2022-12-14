using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;

public interface IDishRepository
{
  public Task<Dish> Add(Dish dish);
  public Task Delete(Guid id);
  public Task Update(Dish dish);
  public Task<Dish?> Get(Guid id);
  public Task<IEnumerable<Dish>> GetAll();
}