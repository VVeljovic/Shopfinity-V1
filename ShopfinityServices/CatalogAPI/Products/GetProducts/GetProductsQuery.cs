namespace CatalogAPI.Products.GetProducts
{
    public sealed record GetProductsQuery(int PageNumber, int PageSize)
        : IRequest<GetProductsResponse>;
}
