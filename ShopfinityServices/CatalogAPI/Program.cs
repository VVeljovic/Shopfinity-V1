using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using CatalogAPI.Products.GetProductByCategoryName;
using CoreLibrary.HandlingExceptions;
using CoreLibrary.SharedSettings;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddValidatorsFromAssemblyContaining<GetProductsByCategoryNameQueryValidator>();


builder.Services.AddMarten(config =>
    config.Connection(builder.Configuration.GetConnectionString("Postgres")!));

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var awsSettings = builder.Configuration.GetSection("AWS").Get<AWSSettings>();
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>(options =>
{
    var credentials = new BasicAWSCredentials(awsSettings.AccessKey, awsSettings.SecretKey);
    var region = RegionEndpoint.USEast1;
    return new AmazonSQSClient(credentials, region);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseExceptionHandler(options => { });

app.MapCarter();
app.Run();

