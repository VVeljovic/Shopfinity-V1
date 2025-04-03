
namespace CatalogAPI.Products.GetProducts
{
    public class GetProductsQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("get-products/{pageNumber}/{pageSize}",
                async (int pageNumber, int pageSize, ISender sender) =>
            {
                var query = new GetProductsQuery(pageNumber, pageSize);

                var response = sender.Send(query);

                return Results.Ok(response);
            }).WithName("GetProducts")
              .WithSummary("Endpoint retrieves products with pagination");
        }
    }
}
