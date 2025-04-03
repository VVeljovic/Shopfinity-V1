using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using CoreLibrary.SharedModels;
using System.Text.Json;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace OrderConfirmationLambda;

public class Function
{


    private readonly IAmazonS3 s3Client;
    public Function()
    {
        s3Client = new AmazonS3Client();
    }


    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach(var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");

        var product = JsonSerializer.Deserialize<Product>(message.Body);
        context.Logger.LogInformation($"Started storing message to queue -- productId {product.Id}");
        
        var response = await UploadToS3($"{product.Category.FirstOrDefault()}/{product.Id}", message.Body);

        context.Logger.LogInformation($"Storing message finished {response.HttpStatusCode}");

        await Task.CompletedTask;
    }

    private async Task<PutObjectResponse> UploadToS3(string fileName, string order)
    {

        var putRequest = new PutObjectRequest
        {
            BucketName = "shopfinity-orders",
            Key = fileName,
            ContentBody = order,
            ContentType = "application/json",
        };

        return await s3Client.PutObjectAsync(putRequest);
    }
}