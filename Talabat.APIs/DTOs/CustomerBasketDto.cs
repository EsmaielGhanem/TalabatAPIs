using Microsoft.Build.Framework;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }

    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    
    
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }

    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }

    
}