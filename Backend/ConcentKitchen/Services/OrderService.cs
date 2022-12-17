using ConcentKitchen.Models;
using ConcentKitchen.Repository;

namespace ConcentKitchen.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        try
        {
            var orderExist = await _repository.Get(order.OrderId);
            if (orderExist is not null) throw new InvalidOperationException("Este pedido já existe");

            var output = await _repository.Add(order);
            return output;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }

    public async Task DeleteOrder(Guid id)
    {
        try
        {
            var orderExist = await _repository.Get(id);
            if (orderExist is null) throw new InvalidOperationException("Este pedido não existe");

            await _repository.Delete(id);
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        try
        {
            var orders = await _repository.GetAll();
            if (!orders.Any()) throw new InvalidOperationException("Não existem pedidos cadastrados");

            return orders;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<Order> GetOrder(string id)
    {
        try
        {
            var order = await _repository.Get(new Guid(id));
            if (order == null) throw new InvalidOperationException("Este pedido não existe");

            return order;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        try
        {
            var orderExist = await _repository.Get(order.OrderId);
            if (orderExist == null) throw new InvalidOperationException("Este pedido não existe");

            orderExist.CompletionDeadline = order.CompletionDeadline;
            orderExist.OrderTime = order.OrderTime;
            orderExist.Status = order.Status;
            orderExist.TotalPrice = order.TotalPrice;

            await _repository.Update(orderExist);

            var updatedOrder = await _repository.Get(order.OrderId);
            return updatedOrder!;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }

    }
}