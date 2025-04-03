using CoreLibrary.SharedModels;

namespace CatalogAPI.Products.GetProductByCategoryName
{
    public sealed record GetProductsByCategoryNameQuery(string CategoryName)
        : IRequest<List<Product>>;
}
