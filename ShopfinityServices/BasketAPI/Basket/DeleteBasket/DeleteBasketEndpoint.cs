
using BasketAPI.Basket.GetBasket;

namespace BasketAPI.Basket.DeleteBasket
{
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("delete-basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);

                var response = await sender.Send(command);

                return Results.Ok(response);
            })
            .WithName("DeleteBasketByUsername")
            .Produces<DeleteBasketCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Delete basket by Username"); 
        }
    }
}
