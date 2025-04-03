using CoreLibrary.SharedModels;

namespace CatalogAPI.Products.GetProductById
{
    public class GetProductByIdQueryHandler(IDocumentSession session)
        : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                .Where(x => x.Id == request.Id)
                .FirstAsync();

            var response = product.Adapt<GetProductByIdQueryResponse>();

            return response;
        }
    }
}
