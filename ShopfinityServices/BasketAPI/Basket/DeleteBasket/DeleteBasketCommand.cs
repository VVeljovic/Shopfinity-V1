namespace BasketAPI.Basket.DeleteBasket
{
    public sealed record DeleteBasketCommand(string Username) : IRequest<DeleteBasketCommandResponse>;
}
