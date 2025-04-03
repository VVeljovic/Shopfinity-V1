
using BasketAPI.Data;

namespace BasketAPI.Basket.DeleteBasket
{
    public sealed class DeleteBasketCommandHandler(IBasketRepository basketRepository) : IRequestHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
    {
        public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await basketRepository.DeleteBasket(request.Username);

            return new DeleteBasketCommandResponse(isDeleted);
        }
    }
}
