
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using CoreLibrary.SharedModels;
using System.Text.Json;

namespace CatalogAPI.Products.CreateProduct
{
    public class CreateProductCommandHandler(IDocumentSession session, IAmazonSQS sqsClient)
        : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();
            product.Id = Guid.NewGuid().ToString();

            session.Store(product);
            await session.SaveChangesAsync();

            await SendMessageOnQueueAsync(product);

            return new CreateProductResponse(Id: product.Id);
        }

        private async Task<SendMessageResponse> SendMessageOnQueueAsync(Product product)
        {
            var productSerialized = JsonSerializer.Serialize(product);

            var messageRequest = new SendMessageRequest()
            {
                QueueUrl = Constants.Constants.AWS.SQS.QueueUrl,
                MessageBody = productSerialized
            };
            return await sqsClient.SendMessageAsync(messageRequest);
        }
    }
}
