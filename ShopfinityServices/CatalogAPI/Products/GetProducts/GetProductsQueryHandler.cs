using CoreLibrary.HandlingExceptions.Exceptions;
using CoreLibrary.SharedModels;
using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts
{
    public class GetProductsQueryHandler(IDocumentSession session)
        : IRequestHandler<GetProductsQuery, GetProductsResponse>
    {
        public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedList = await session.Query<Product>()
                .ToPagedListAsync(request.PageNumber, request.PageSize);

            if (pagedList.Count == 0)
            {
                throw new NotFoundException("No products exist.");
            }

            var response = new GetProductsResponse(pagedList.ToList());


            return response;
        }
    }
}
