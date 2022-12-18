using ConcentKitchen.Models;
using ConcentKitchen.Repository;

namespace ConcentKitchen.Services;
public class DishService : IDishService
{
    private readonly IDishRepository _repository;
    public DishService(IDishRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dish> CreateDish(Dish dish)
    {
        try
        {
            var dishExist = await _repository.Get(dish.DishId);
            if (dishExist is not null) throw new InvalidOperationException("Este prato já existe");

            var output = await _repository.Add(dish);
            return output;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }

    public async Task DeleteDish(Guid id)
    {
        try
        {
            var dishExist = await _repository.Get(id);
            if (dishExist is null) throw new InvalidOperationException("Este prato não existe");

            await _repository.Delete(id);
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<IEnumerable<Dish>> GetAllDishes()
    {
        try
        {
            var dishes = await _repository.GetAll();
            if (!dishes.Any()) throw new InvalidOperationException("Não existem pratos cadastrados");

            return dishes;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<Dish> GetDish(string id)
    {
        try
        {
            var dish = await _repository.Get(new Guid(id));
            if (dish == null) throw new InvalidOperationException("Este prato não existe");

            return dish;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
    }

    public async Task<Dish> UpdateDish(Dish dish)
    {
        try
        {
            var dishExist = await _repository.Get(dish.DishId);
            if (dishExist == null) throw new InvalidOperationException("Este prato não existe");

            dishExist.DishName = dish.DishName;
            dishExist.DishIngredients = dish.DishIngredients;
            dishExist.DishCategory = dish.DishCategory;
            dishExist.DishPrice = dish.DishPrice;

            await _repository.Update(dishExist);

            var updatedDish = await _repository.Get(dish.DishId);
            return updatedDish!;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }

    }
}