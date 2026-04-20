namespace Talabat.Core.Entities.Order_Aggregate;

public class Order : BaseEntity
{
    // There Must be empty patameterless CTOR  ==> For EF 
    public Order()
    {
        
    }
    public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntedId)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Items = items;
        SubTotal = subTotal;
        PaymentIntedId = paymentIntedId;
    }
    public string BuyerEmail { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    public OrderStatus Status { get; set; } = OrderStatus.pending; 

    public Address ShippingAddress { get; set; }

    public DeliveryMethod DeliveryMethod { get; set; }  // Navigational PROP ONE 


    public ICollection<OrderItem> Items { get; set; }  // Navigaitional prop MANY    

    public decimal SubTotal { get; set; } 

    public string PaymentIntedId { get; set; }


    public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;




}