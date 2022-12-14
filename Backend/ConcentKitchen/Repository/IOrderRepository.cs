using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;

public interface IOrderRepository
{
  public Task<Order> Add(Order order);
  public Task Delete(Guid id);
  public Task Update(Order order);
  public Task<Order?> Get(Guid id);
  public Task<IEnumerable<Order>> GetAll();
}