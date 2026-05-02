using Ecom.Core.Entities.CustomersBasket;
using Ecom.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecom.infrastructure.Reposities
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDatabase _database;

        public CustomerBasketRepository(IConnectionMultiplexer redis) 
        {
            _database = redis.GetDatabase();
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            return _database.KeyDeleteAsync(id);
        }

        public async Task<CustomersBasket?> GetBasketAsync(string id)
        {
            var result = await _database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<CustomersBasket>((string)result!);
            }
            return null;
        }

        public async Task<CustomersBasket?> UpdateBasketAsync(CustomersBasket basket)
        {
            var _basket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(3));
            if (_basket)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;

        }
    }
}
