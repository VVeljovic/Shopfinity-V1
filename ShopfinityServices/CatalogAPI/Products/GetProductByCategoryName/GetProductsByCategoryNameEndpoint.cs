namespace CatalogAPI.Products.GetProductByCategoryName
{
    public sealed class GetProductsByCategoryNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("get-products-by-category-name/{category}", async (string category, ISender sender) =>
            {
                var request = new GetProductsByCategoryNameQuery(category);

                var result = await sender.Send(request);

                return Results.Ok(result);
            })
              .WithName("Get products by category name")
              .WithSummary("For specific category name retrieve all products");
        }
    }
}
