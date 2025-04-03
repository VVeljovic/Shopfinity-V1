using CoreLibrary.HandlingExceptions.Exceptions;
using CoreLibrary.SharedModels;

namespace CatalogAPI.Products.GetProductByCategoryName
{
    public sealed class GetProductsByCategoryNameQueryHandler(IDocumentSession session) :
        IRequestHandler<GetProductsByCategoryNameQuery, List<Product>>
    {
        public async Task<List<Product>> Handle(GetProductsByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var productsDB = await session.Query<Product>()
                .Where(x => x.Category.Contains(request.CategoryName))
                .ToListAsync();

            if (productsDB.Count == 0)
            {
                throw new NotFoundException("Products for this category name do not exist");
            }

            return productsDB.ToList();

        }
    }
}
