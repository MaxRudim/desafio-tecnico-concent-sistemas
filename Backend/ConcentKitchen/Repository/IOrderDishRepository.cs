using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;

public interface IOrderDishRepository
{
  public Task<OrderDish> Add(OrderDish orderdish);
  public Task Delete(Guid id);
  public Task Update(OrderDish orderdish);
  public Task<OrderDish?> Get(Guid id);
  public Task<IEnumerable<OrderDish>> GetAll();
}