using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;
public class OrderRepository : IOrderRepository
{
    protected readonly KitchenContext _context;
    public OrderRepository(KitchenContext context)
    {
        _context = context;
    }

    public async Task<Order> Add(Order order)
    {
        _context.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Orders!.Single(p => p.OrderId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Order order)
    {
        _context.ChangeTracker.Clear();

        _context.Update(order);

        await _context.SaveChangesAsync();
    }   

    public async Task<Order?> Get(Guid id)
    {
        var order = await _context.Orders!.AsNoTracking().FirstOrDefaultAsync(a => a.OrderId == id);

        return order;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        var orders = await _context.Orders!.ToListAsync();

        return orders;
    }

}