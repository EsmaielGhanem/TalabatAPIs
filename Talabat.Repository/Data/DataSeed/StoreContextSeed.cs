using System.Text.Json;
using Microsoft.Extensions.Logging;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.DataSeed;

public class StoreContextSeed
{
    public static async Task SeedASync(StoreContext context , ILoggerFactory loggerFactory)
    {

        try
        {

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("D:\\New folder\\SOM3A\\ROUTE\\MY TASKS\\1-API\\Talabat\\Talabat.Repository\\Data\\DataSeed\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                foreach (var brand in brands)
                {
                    context.Set<ProductBrand>().Add(brand);
                }

            }

            if (!context.ProductTypes.Any())
            {
                var TypesDate = File.ReadAllText("D:\\New folder\\SOM3A\\ROUTE\\MY TASKS\\1-API\\Talabat\\Talabat.Repository\\Data\\DataSeed\\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesDate);

                foreach (var type in Types)
                {
                    context.Set<ProductType>().Add(type);
                }
               

            }

            if (!context.Products.Any())
            {
                var ProductData = File.ReadAllText("D:\\New folder\\SOM3A\\ROUTE\\MY TASKS\\1-API\\Talabat\\Talabat.Repository\\Data\\DataSeed\\products.json");
            
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            
                foreach (var product in Products)
                {
                    context.Set<Product>().Add(product);
                }
            
            
            }
            
            if (!context.DeliveryMethods.Any())
            {
                var delivetyMethodData = File.ReadAllText("D:\\New folder\\SOM3A\\ROUTE\\MY TASKS\\1-API\\Talabat\\Talabat.Repository\\Data\\DataSeed\\delivery.json");
            
                var deliveryData = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivetyMethodData);
            
                foreach (var delivery in deliveryData)
                {
                    context.Set<DeliveryMethod>().Add(delivery);
                }
            
            
            }
            
            await context.SaveChangesAsync();

        }
        
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>(); 
            logger.LogError(ex, $"❌ Error in Seeding: {ex.Message}"); 
        }
    }
    
}