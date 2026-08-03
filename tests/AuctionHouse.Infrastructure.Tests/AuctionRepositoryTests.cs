// In-memory stand in for a database
// Implements IAuctionRepository with six methods 
using AuctionHouse.Domain.Entities;
using AuctionHouse.Infrastructure.Repositories;

namespace AuctionHouse.Infrastructure.Tests;

public class AuctionRepositoryTests
{
    [Fact]
    public async Task AddAsync_ThenGetByIdAsync_ReturnsSameAuction()
    {
        // Make repository
        var repo = new AuctionRepository();

        // Create arguments for test auction
        Guid sellerid = Guid.NewGuid(); 
        string title = "Auction Title";
        string description = "Placeholder description";
        decimal startingprice =  100.00m;
        DateTime starttime = DateTime.UtcNow; 
        DateTime endtime = starttime.FromDays(7); 

        Auction testAuction = Auction.Create(sellerid, title, description, startingprice, starttime, endtime);        

        await repo.AddAsync(testAuction);
        var result = await repo.GetByIdAsync(testAuction.Id);

        Assert.Equal(testAuction.Id, result.Id);
    }
}
