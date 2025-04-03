using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using CoreLibrary.SharedSettings;
using PaymentAPI.MessageProcessors;

var builder = WebApplication.CreateBuilder(args);
var awsSettings = builder.Configuration.GetSection("AWS").Get<AWSSettings>();

builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>(options =>
{
    var credentials = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey);
    var region = RegionEndpoint.USEast1;
    return new AmazonSQSClient(credentials, region);
});

builder.Services.AddHostedService<CreateOrderMessageProcessor>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
