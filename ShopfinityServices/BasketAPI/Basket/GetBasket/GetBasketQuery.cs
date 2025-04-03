using MediatR;

namespace BasketAPI.Basket.GetBasket
{
    public sealed record GetBasketQuery(string UserName) : IRequest<GetBasketQueryResponse>;
}
