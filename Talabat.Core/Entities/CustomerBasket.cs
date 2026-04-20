namespace Talabat.Core.Entities;

public class CustomerBasket
{

    public string Id { get; set; }

    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }

    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }

    public CustomerBasket(string id)
    {
        Id = id; 
    }
}