using BasketAPI.Models;

namespace BasketAPI.Basket.CreateBasket
{
    public sealed record CreateBasketCommand(ShoppingCart ShoppingCart) : IRequest<CreateBasketCommandResponse>;
}
