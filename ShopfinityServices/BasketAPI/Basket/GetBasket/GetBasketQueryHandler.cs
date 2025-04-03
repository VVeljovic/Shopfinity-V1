using BasketAPI.Data;
using BasketAPI.Models;
using Mapster;
using MediatR;

namespace BasketAPI.Basket.GetBasket
{
    public sealed class GetBasketQueryHandler(IBasketRepository basketRepository) : IRequestHandler<GetBasketQuery, GetBasketQueryResponse>
    {
        public async Task<GetBasketQueryResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.UserName);
            return new GetBasketQueryResponse(basket);
        }
    }
}
