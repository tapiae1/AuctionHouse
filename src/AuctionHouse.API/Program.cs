/*Since this is the first time that I worked with ASP .NET Core, there are some concepts that I just don't really get.
 The biggest one that I had trouble with are the life times to the different services in the container. 
 So a lot of the time that I had spent working on this file is just understanding the ASP .NET Core services. */

// The lifetime of the User and Bid repositories should be Singleton because they need to persist through multiple requests. 
// Otherwise, there will be a new database everytime a new request is made. 
using AuctionHouse.Domain.Repositories;
using AuctionHouse.Infrastructure.Repositories; 
using AuctionHouse.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
builder.Services.AddSingleton<IBidRepository, BidRepository>();
builder.Services.AddScoped<PlaceBidService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();




//Jade-Monet Wiglesworth loves Eduardo Alberto Tapia-Gonzalez very much! <3