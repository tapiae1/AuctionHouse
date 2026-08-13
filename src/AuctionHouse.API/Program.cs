/*Since this is the first time that I worked with ASP .NET Core, there are some concepts that I just don't really get.
 The biggest one that I had trouble with are the life times to the different services in the container. 
 So a lot of the time that I had spent working on this file is just understanding the ASP .NET Core services. */

// The lifetime of the User and Bid repositories should be Singleton because they need to persist through multiple requests. 
// Otherwise, there will be a new database everytime a new request is made. 
using AuctionHouse.Domain.Repositories;
using AuctionHouse.Infrastructure.Repositories; 
using AuctionHouse.Application.Services;
using AuctionHouse.Domain.Exceptions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();// TODO: change this whenever there is a real database. 
builder.Services.AddSingleton<IBidRepository, BidRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<PlaceBidService>();
builder.Services.AddScoped<CreateAuctionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ***************************************Endpoints (server side code)********************************************8


// ****** CREATE USER ******   
app.MapPost("/users", async (CreateUserRequest request, CreateUserService service) =>
{
    // TODO: Add some validation
    var user = await service.CreateUserAsync(request.name, request.email, request.passwordHash);
}); 

// ****** CREATE AUCTION ******
app.MapPost("/auctions", async (CreateAuctionRequest request, CreateAuctionService service) =>
{
    try
    {
        var auction = await service.CreateAuctionAsync(request.sellerId, request.title, request.description, request.startingPrice, request.startTime, request.endTime);
        return Results.Created($"/auctions/{auction.Id}", auction);
    }
    catch (NotFoundException ex) // Seller doesn't exist 
    {
        Console.WriteLine(ex.Message);
        return Results.NotFound(ex.Message);
    }
});

// ****** PLACE BID ******
app.MapPost("/auctions/{auctionId}/bids", async (Guid auctionId, PlaceBidRequest request, PlaceBidService service) =>
{
    // Wrapping in a try/catch clause because the bid might not be valid, or the auction was not found.
    try
    {
        var bid = await service.PlaceBidAsync(auctionId, request.BidderId, request.Amount);
        return Results.Ok(bid);
    }
    catch (NotFoundException ex)
    {
        Console.WriteLine(ex.Message); // Won't scale well whenever more endpoints are added. 
        return Results.NotFound(ex.Message);
    }
    catch (DomainException ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest(ex.Message);
    }
});

// ****** GET AUCTION ******
app.MapGet("/auctions/{auctionId}", async (Guid auctionId, IAuctionRepository repository) =>
{
    var auction = await repository.GetByIdAsync(auctionId);
    if (auction == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(auction);
});

app.Run();  

// DTOs (Data Transfer Objects)
record CreateAuctionRequest(Guid sellerId, string title, string description, decimal startingPrice, DateTime startTime, DateTime endTime);
record PlaceBidRequest(Guid BidderId, decimal Amount);
record CreateUserRequest(string name, string email, string passwordHash);


//Jade-Monet Wiglesworth loves Eduardo Alberto Tapia-Gonzalez very much! <3