using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());

        builder.Property(O => O.Status)
            .HasConversion(
                OStatus => OStatus.ToString() ,  
                status => Enum.Parse<OrderStatus>(status)
            );


        builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(O => O.SubTotal).HasColumnType("decimal(18, 2)");

        
        
        



    }
}