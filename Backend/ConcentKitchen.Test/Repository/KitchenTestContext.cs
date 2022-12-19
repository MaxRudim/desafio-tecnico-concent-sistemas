using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;
using Microsoft.Extensions.DependencyInjection;
using ConcentKitchen.Repository;

namespace ConcentKitchen.Test.Repository;

public class KitchenTestContext : KitchenContext
{
    public DbSet<Client> Client1 { get; set; } = null!;
    public DbSet<Dish> Dish1 { get; set; } = null!;
    public DbSet<Order> Order1 { get; set; } = null!;
    public DbSet<OrderDish> OrderDish1 { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        optionsBuilder.UseInMemoryDatabase("Client1").UseInternalServiceProvider(serviceProvider);
        optionsBuilder.UseInMemoryDatabase("Dish1").UseInternalServiceProvider(serviceProvider);
        optionsBuilder.UseInMemoryDatabase("Order1").UseInternalServiceProvider(serviceProvider);
        optionsBuilder.UseInMemoryDatabase("OrderDish1").UseInternalServiceProvider(serviceProvider);
    }
    protected override void OnModelCreating(ModelBuilder mb)
    {
       mb.Entity<Order>()
        .HasKey(o => new { o.OrderId });

      mb.Entity<Client>()
        .HasKey(cl => new { cl.ClientId });

      mb.Entity<Dish>()
        .HasKey(d => new { d.DishId });
      
      mb.Entity<OrderDish>()
        .HasKey(od => new { od.Id });     

      mb.Entity<OrderDish>()
        .HasMany(d => d.Dish);

      mb.Entity<OrderDish>()
        .HasMany(d => d.Order);
    }

}