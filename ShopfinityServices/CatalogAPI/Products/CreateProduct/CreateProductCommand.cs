namespace CatalogAPI.Products.CreateProduct
{
    public sealed record CreateProductCommand
        (string Name, List<string> Category,
        string Description, string ImageFile,
        double Price) : IRequest<CreateProductResponse>;
}
