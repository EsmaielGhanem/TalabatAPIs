using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications;

public class OrderWithItemAndDeliveryMethodSpecification : BaseSpecification<Order>
{
    public OrderWithItemAndDeliveryMethodSpecification(string buyerEmail) 
    :base(O => O.BuyerEmail == buyerEmail)
    {
        Includes.Add(O => O.DeliveryMethod); // Eager
        Includes.Add(O => O.Items); // Eager
        
        
    }
    
    public OrderWithItemAndDeliveryMethodSpecification(int orderId , string buyerEmail) 
        :base(O => (O.BuyerEmail == buyerEmail && O.Id == orderId))
    {
        Includes.Add(O => O.DeliveryMethod); // Eager
        Includes.Add(O => O.Items); // Eager
        
        
    }

    
}