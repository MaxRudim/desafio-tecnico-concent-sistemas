using ConcentKitchen.Models;

namespace ConcentKitchen.Services;

public interface IOrderService
{
    public Task<Order> CreateOrder(Order order);
    public Task DeleteOrder(Guid id);
    public Task<Order> UpdateOrder(Order order);
    public Task<IEnumerable<Order>> GetAllOrders();
    public Task<Order> GetOrder(string id);
    public Task<ICollection<Order>> GetOrdersByClient(Guid clientid);

}