using Talabat.Core.Entities;

namespace Talabat.APIs.Interfaces.IRepositories;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string Id);

    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);


    Task<bool> DeleteBasket(string BasketId);
}