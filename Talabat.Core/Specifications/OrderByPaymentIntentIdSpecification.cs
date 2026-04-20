using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications;

public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpecification(string paymentIntentId)
    :base(O => O.PaymentIntedId ==  paymentIntentId)
    {
        
    }
}