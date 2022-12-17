using ConcentKitchen.Models;
using ConcentKitchen.Repository;

namespace ConcentKitchen.Services;
public class OrderDishService : IOrderDishService
{
    private readonly IOrderDishRepository _repository;
    private readonly IOrderRepository _orderRepository;
    private readonly IDishRepository _dishRepository;
    public OrderDishService(IOrderDishRepository repository, IOrderRepository orderRepository, IDishRepository dishRepository)
    {
        _repository = repository;
        _orderRepository = orderRepository;
        _dishRepository = dishRepository;
    }

    public async Task<OrderDish> CreateOrderDish(OrderDish orderdish)
    {
        try
        {
            var orderDishExist = await _repository.Get(orderdish.Id);
            if (orderDishExist is not null) throw new InvalidOperationException("Esta relação ordem/prato já existe");

            var orderExist = await _orderRepository.Get(orderdish.OrderId);
            if (orderExist is null) throw new InvalidOperationException("Este pedido não existe");

            var dishExist = await _dishRepository.Get(orderdish.DishId);
            if (dishExist is null) throw new InvalidOperationException("Este prato não existe");

            var output = await _repository.Add(orderdish);
            return output;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }

    public async Task DeleteOrderDish(Guid id)
    {
        try
        {
            var orderDishExist = await _repository.Get(id);
            if (orderDishExist is null) throw new InvalidOperationException("Esta relação ordem/prato não existe");

            await _repository.Delete(id);
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<IEnumerable<OrderDish>> GetAllOrderDishes()
    {
        try
        {
            var orderDishes = await _repository.GetAll();
            if (!orderDishes.Any()) throw new InvalidOperationException("Não existem relações ordem/prato cadastradas");

            return orderDishes;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<OrderDish> GetOrderDish(string id)
    {
        try
        {
            var orderDish = await _repository.Get(new Guid(id));
            if (orderDish == null) throw new InvalidOperationException("Esta relação ordem/pedido não existe");

            return orderDish;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
    }

}