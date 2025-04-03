namespace CatalogAPI.Products.GetProductById
{
    public sealed record GetProductByIdQuery(string Id)
        : IRequest<GetProductByIdQueryResponse>;
}
