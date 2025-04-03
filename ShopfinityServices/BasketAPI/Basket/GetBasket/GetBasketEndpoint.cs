using Carter;
using Mapster;
using MediatR;

namespace BasketAPI.Basket.GetBasket
{
    public sealed class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                return Results.Ok(result);
            })
            .WithName("GetBasketByUsername")
            .Produces<GetBasketQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket by Username");

        }
    }
}
