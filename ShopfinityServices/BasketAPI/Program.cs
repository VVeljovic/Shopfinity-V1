using BasketAPI.Data;
using BasketAPI.Models;
using CoreLibrary.HandlingExceptions;
using Marten;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddMarten(config =>
        {
            config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
            config.Connection(builder.Configuration.GetConnectionString("Postgres")!);
        })
    .UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapCarter();
app.Run();

