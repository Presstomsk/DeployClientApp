using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) {}
}