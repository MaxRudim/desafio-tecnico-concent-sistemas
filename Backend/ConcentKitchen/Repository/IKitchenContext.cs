using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository
{
    public interface IKitchenContext
    {
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Dish>? Dishes { get; set; }
    public DbSet<Order>? Orders { get; set; }

    public int SaveChanges();

    }
}