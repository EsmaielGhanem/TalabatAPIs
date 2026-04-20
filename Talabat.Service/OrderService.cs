using Talabat.APIs.Interfaces.IRepositories;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Service;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;

    private readonly IBasketRepository _basketRepository;
    // private readonly IGenericRepository<Product> _productRepo;
    // private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
    // private readonly IGenericRepository<Order> _orderRepo;

    public OrderService(
        IBasketRepository basketRepository ,
        IUnitOfWork unitOfWork, IPaymentService paymentService

        // , IGenericRepository<Product> productRepo
        // ,IGenericRepository<DeliveryMethod>deliveryMethodRepo 
        // , IGenericRepository<Order> orderRepo
        
        )
    {
        _unitOfWork = unitOfWork;
        _paymentService = paymentService;
        _basketRepository = basketRepository;
        // _productRepo = productRepo;
        // _deliveryMethodRepo = deliveryMethodRepo;
        // _orderRepo = orderRepo;
    }
    public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
    {
        // Get Basket From Basket Repo 
        var basket = await _basketRepository.GetBasketAsync(basketId);
     
        // Get Selected Items at Basket From Products Repo 
        var orderItems = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var product = await  _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
            var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);
            orderItems.Add(orderItem);

        }
    
        // Calclate SubTotal 
        var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
     
        // Get DeliveryMethod From DeliveryMethod Repo 
        var deliveryMethod =   await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId); 
     
        // Create Order 

        var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
        var existingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

        if (existingOrder != null)
        {
             _unitOfWork.Repository<Order>().Delete(existingOrder);
             await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        } 
        
        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal , basket.PaymentIntentId);

        await _unitOfWork.Repository<Order>().CreateAsync(order);
     
        // Save To DataBase [TODO]
        
       var result = await _unitOfWork.Complete();

       
       if(result <= 0) return null;
        return order;


    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrderWithItemAndDeliveryMethodSpecification( buyerEmail);
        var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

        return orders;
    }

    public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
    {
        var spec = new OrderWithItemAndDeliveryMethodSpecification(orderId, buyerEmail);
        var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        return deliveryMethod; 
    }
}