using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;
public class OrderDishRepository : IOrderDishRepository
{
    protected readonly KitchenContext _context;
    public OrderDishRepository(KitchenContext context)
    {
        _context = context;
    }

    public async Task<OrderDish> Add(OrderDish orderdish)
    {
        _context.Add(orderdish);

        await _context.SaveChangesAsync();

        return orderdish;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.OrderDishes!.Single(p => p.Id == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(OrderDish orderdish)
    {
        _context.ChangeTracker.Clear();

        _context.Update(orderdish);

        await _context.SaveChangesAsync();
    }   

    public async Task<OrderDish?> Get(Guid id)
    {
        var orderdish = await _context.OrderDishes!.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        return orderdish;
    }

    public async Task<IEnumerable<OrderDish>> GetAll()
    {
        var orderdishes = await _context.OrderDishes!.ToListAsync();

        return orderdishes;
    }

}