using ConcentKitchen.Models;

namespace ConcentKitchen.Services;

public interface IOrderDishService
{
    public Task<OrderDish> CreateOrderDish(OrderDish orderdish);
    public Task DeleteOrderDish(Guid id);
    public Task<IEnumerable<OrderDish>> GetAllOrderDishes();
    public Task<OrderDish> GetOrderDish(string id);

}