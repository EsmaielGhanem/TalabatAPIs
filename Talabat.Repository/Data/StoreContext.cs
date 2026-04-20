using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repository.Data.Configurations;

namespace Talabat.Repository.Data;

public class StoreContext : DbContext   // Package : ms.EF Core .SqlServer
{
    // dependency Injection
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfiguration(new ProductConfiguration());
            
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   // IEntityTypeConfiguration 
     }
    
    public DbSet<Product> Products { set; get; }  
    public DbSet<ProductBrand> ProductBrands { set; get; }  
    public DbSet<ProductType> ProductTypes { set; get; }

    public DbSet<Order> Orders { get; set; }      
    public DbSet<OrderItem> OrderItems { get; set; }  
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }  
}