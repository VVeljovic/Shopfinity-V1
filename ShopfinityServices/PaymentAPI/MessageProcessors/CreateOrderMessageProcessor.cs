
using Amazon.SQS;
using Amazon.SQS.Model;
using CoreLibrary.SharedConstants;

namespace PaymentAPI.MessageProcessors
{
    public sealed class CreateOrderMessageProcessor(IAmazonSQS sqsClient) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = Constants.AWS.SQS.QueueUrl
                };

                var response = await sqsClient.ReceiveMessageAsync(request);
                foreach (var message in response.Messages) 
                {
                    Console.WriteLine(message.Body);
                    await sqsClient.DeleteMessageAsync(Constants.AWS.SQS.QueueUrl, message.ReceiptHandle, stoppingToken);
                }
            }
        }
    }
}
