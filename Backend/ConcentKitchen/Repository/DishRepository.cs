using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;
public class DishRepository : IDishRepository
{
    protected readonly KitchenContext _context;
    public DishRepository(KitchenContext context)
    {
        _context = context;
    }

    public async Task<Dish> Add(Dish dish)
    {
        _context.Add(dish);

        await _context.SaveChangesAsync();

        return dish;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Dishes!.Single(p => p.DishId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Dish dish)
    {
        _context.ChangeTracker.Clear();

        _context.Update(dish);

        await _context.SaveChangesAsync();
    }   

    public async Task<Dish?> Get(Guid id)
    {
        var dish = await _context.Dishes!.AsNoTracking().FirstOrDefaultAsync(a => a.DishId == id);

        return dish;
    }

    public async Task<IEnumerable<Dish>> GetAll()
    {
        var dishes = await _context.Dishes!.ToListAsync();

        return dishes;
    }

}