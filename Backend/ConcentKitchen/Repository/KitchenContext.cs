using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;
public class KitchenContext : DbContext, IKitchenContext
{
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Dish>? Dishes { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderDish>? OrderDishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (!optionsBuilder.IsConfigured)
        {
          IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

          optionsBuilder.UseSqlServer(configuration.GetConnectionString("KitchenDatabase"));
        }
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
       mb.Entity<Order>()
        .HasKey(o => new { o.ClientId });

      mb.Entity<Order>()
        .HasOne(cl => cl.Client)
        .WithMany(o => o.Orders)
        .HasForeignKey(cl => cl.ClientId);

      mb.Entity<OrderDish>()
        .HasMany(d => d.Dish);

      mb.Entity<OrderDish>()
        .HasMany(d => d.Order);
    }
}