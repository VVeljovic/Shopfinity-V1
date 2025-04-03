using BasketAPI.Models;
using CoreLibrary.HandlingExceptions.Exceptions;
using Marten;

namespace BasketAPI.Data
{
    public sealed class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new NotFoundException($"Basket for {userName} not found") : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            session.Store(cart);

            await session.SaveChangesAsync();

            return cart;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
        {
            session.Delete<ShoppingCart>(userName);

            await session.SaveChangesAsync();

            return true;
        }

    }
}
