
using BasketAPI.Data;
using CoreLibrary.HandlingExceptions.Exceptions;

namespace BasketAPI.Basket.CreateBasket
{
    public sealed class CreateBasketCommandHandler(IBasketRepository repository) : IRequestHandler<CreateBasketCommand, CreateBasketCommandResponse>
    {
        public async Task<CreateBasketCommandResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var response = await repository.StoreBasket(request.ShoppingCart);

            var result = new CreateBasketCommandResponse(response);
            return result;

        }
    }
}
