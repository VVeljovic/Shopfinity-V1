namespace CatalogAPI.Products.GetProductById
{
    public sealed record GetProductByIdQueryResponse(string Id, string Name, List<string> Category,
        string Description, string ImageFile,
        double Price);
}
