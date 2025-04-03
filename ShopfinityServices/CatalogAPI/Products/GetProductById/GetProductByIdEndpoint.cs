
namespace CatalogAPI.Products.GetProductById
{
    public sealed class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("get-product-by-id/{Id}", async (string Id, ISender sender) =>
            {
                var getProductByIdQuery = new GetProductByIdQuery(Id);

                var response = await sender.Send(getProductByIdQuery);

                return Results.Ok(response);

            }).WithName("GetProductById")
              .WithSummary("For given Id returns a specific product");
        }
    }
}
