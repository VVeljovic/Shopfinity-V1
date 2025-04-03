namespace CatalogAPI.Products.CreateProduct
{
    public sealed class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("create-product", async (CreateProductCommand createProduct,
                ISender sender) =>
            {
                var response = await sender.Send(createProduct);

                return Results.Created($"/products/{response.Id}", response);
            })
              .WithName("CreateProduct")
              .WithSummary("Endpoint for creating new product")
              .WithDescription("Request contains product's properties " +
                                        "and response contains a new product Id");
        }
    }
}
