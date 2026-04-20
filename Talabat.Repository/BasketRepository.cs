using System.Text.Json;
using StackExchange.Redis;
using Talabat.APIs.Interfaces.IRepositories;
using Talabat.Core.Entities;

namespace Talabat.Repository;

public class BasketRepository : IBasketRepository
{   
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var basket = await _database.StringGetAsync(Id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var CreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket) , TimeSpan.FromDays(30));
        if (CreatedOrUpdated == false) return null;
        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasket(string BasketId)
    {
        return await _database.KeyDeleteAsync(BasketId);
    }
}