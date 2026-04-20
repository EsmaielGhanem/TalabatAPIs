using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.APIs.Interfaces.IRepositories;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration configuration , IBasketRepository basketRepository , IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
    {

        StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];
        var basket = await _basketRepository.GetBasketAsync(basketId);
        if (basket == null) return null;

        var shippingPrice = 0m; 
        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
            shippingPrice = deliveryMethod.Cost;
            basket.ShippingPrice = deliveryMethod.Cost;
        }

        foreach (var item in basket.Items)
        {
            var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            if (item.Price != product.Price) item.Price = product.Price;
        }

        var service = new PaymentIntentService();
        PaymentIntent intent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        { 
            var options = new PaymentIntentCreateOptions() 
            {
                Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100 ) + (long)(shippingPrice * 100) , 
                Currency = "usd" , 
                PaymentMethodTypes = new List<string>(){"card"}
                
            };
            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)(shippingPrice * 100)

            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepository.UpdateBasketAsync(basket);

        return basket; 
        


    }
}