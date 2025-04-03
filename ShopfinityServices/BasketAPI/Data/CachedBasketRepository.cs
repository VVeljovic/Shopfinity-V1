using BasketAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BasketAPI.Data
{
    public sealed class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);

            if (BasketExistsInCache(cachedBasket))
            {
                var basket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
                return basket!;
            }

            return await basketRepository.GetBasket(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await distributedCache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));

            return await basketRepository.StoreBasket(cart, cancellationToken);
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
        {
            await basketRepository.DeleteBasket(userName);

            await distributedCache.RemoveAsync(userName);

            return true;
        }

        private bool BasketExistsInCache(string cachedBasket)
        {
            return string.IsNullOrEmpty(cachedBasket);
        }
    }
}
