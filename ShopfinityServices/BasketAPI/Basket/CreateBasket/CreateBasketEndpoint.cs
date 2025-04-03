
using BasketAPI.Basket.GetBasket;
using BasketAPI.Models;

namespace BasketAPI.Basket.CreateBasket
{
    public sealed class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("create-basket", async (ShoppingCart shoppingCart, ISender sender) =>
            {
                var command = new CreateBasketCommand(shoppingCart);
                var response = await sender.Send(command);

                return Results.Created($"/basket/{response.ShoppingCart.UserName}", response);
            })
            .WithName("Create Basket")
            .Produces<GetBasketQueryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new basket");
        }
    }
}
