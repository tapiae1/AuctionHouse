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
        var auctionRepo = new AuctionRepository();

        // Create arguments for test auction
        var sellerId = Guid.NewGuid(); 
        var title = "Auction Title";
        var description = "Placeholder description";
        var startingPrice =  100.00m;
        var startTime = DateTime.UtcNow; 
        var endtime = startTime.AddDays(7); 
        
        // Create Auction object
        Auction testAuction = Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);        
        
        await auctionRepo.AddAsync(testAuction);
        var result = await auctionRepo.GetByIdAsync(testAuction.Id);
        
        Assert.NotNull(result);
        Assert.Equal(testAuction.Id, result.Id);
    }
    
    [Fact]
    public async Task GetByIdAsync_WhenIdNotFound_ReturnsNull()
    {   
        var auctionRepo = new AuctionRepository();
        var result = await auctionRepo.GetByIdAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ThenGetAllAsync_ReturnsAllAddedAuctions()
    {
        // Arguments 
        var auctionRepo = new AuctionRepository();
        var sellerId = Guid.NewGuid();
        var sellerId2 = Guid.NewGuid();
        var sellerId3 = Guid.NewGuid();
        var title = "Auction Title";
        var description = "Placeholder description";
        var startingPrice = 100.00m;
        var startTime = DateTime.UtcNow;
        var endtime = startTime.AddDays(7);
        
        // Create auctions 
        Auction auction1 = Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);
        Auction auction2 = Auction.Create(sellerId2, title, description, startingPrice, startTime, endtime); 
        Auction auction3 = Auction.Create(sellerId3, title, description, startingPrice, startTime, endtime);
        await auctionRepo.AddAsync(auction1);
        await auctionRepo.AddAsync(auction2); 
        await auctionRepo.AddAsync(auction3);
        
        var result = await auctionRepo.GetAllAsync();
        Assert.NotNull(result);
        Assert.Equal(3, result.Count()); // Possible multiple enumeration TODO: fix whenever the database is made.
        Assert.Contains(auction1, result);  
        Assert.Contains(auction2, result);
        Assert.Contains(auction3, result);
    }
    
    [Fact]
    public async Task GetBySellerIdAsync_WhenIdFound_ReturnsAuction()
    {
        // Arguments 
        var auctionRepo = new AuctionRepository();
        var sellerId = Guid.NewGuid();
        var sellerId1 = Guid.NewGuid();
        var title = "Auction Title";
        var description = "Placeholder description";
        var startingPrice = 100.00m;
        var startTime = DateTime.UtcNow;
        var endtime = startTime.AddDays(7);
        
        // Create auctions 
        var auction =  Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);
        var auction1 = Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);
        var auction2 = Auction.Create(sellerId1, title, description, startingPrice, startTime, endtime);    
        await auctionRepo.AddAsync(auction);
        await auctionRepo.AddAsync(auction1);
        await auctionRepo.AddAsync(auction2);
        
        var allAuctions = await auctionRepo.GetAllAsync();
        var result = await auctionRepo.GetBySellerIdAsync(sellerId);
        Assert.Contains(auction2, allAuctions);
        Assert.Contains(auction1, result);
        Assert.Contains(auction, result); // TODO: Possible multiple enumeration
        Assert.DoesNotContain(auction2, result);
    }
    
    // So I ran into a problem with this function. Since there's no real database right now, testing to see if the update method was a
    // little challenging. Right now Auction is a reference type, and if I do AddAsync(), and my dictionary stores the actual object reference, any
    // mutation method is already reflected inside the memory, bypassing the UpdateAsync() method directly. So for testing this method, I will 
    // skip this test, and only do an insertion via update. 
    [Fact]
    public async Task UpdateAsync_WhenAuctionNotYetAdded_InsertsAuction()
    {
        // Arguments 
        var auctionRepo = new AuctionRepository();
        var sellerId = Guid.NewGuid();
        var title = "Auction Title";
        var description = "Placeholder description";
        var startingPrice = 100.00m;
        var startTime = DateTime.UtcNow;
        var endtime = startTime.AddDays(7);
        
        // Create auction 
        var auction = Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);
        await auctionRepo.UpdateAsync(auction);
        var auctions = await auctionRepo.GetAllAsync();
        
        // Assert it has the updated auction 
        Assert.NotNull(auction);
        Assert.Contains(auction, auctions);
    }
    
    
    
    
    /// <summary>
    ///  This method needs to be worked on. auctions is currently a live view of an in-memory dictionary.
    ///  Instead, there should be a snapshot of the "database," before and after deletion of an auction entry.
    /// I believe this reflects a real life scenario 
    /// </summary>
    [Fact]
    public async Task AddAsync_ThenDeleteAsync_RemovesAuction()
    {
        var auctionRepo = new AuctionRepository();
        var sellerId = Guid.NewGuid();
        var title = "Auction Title";
        var description = "Placeholder description";
        var startingPrice = 100.00m;
        var startTime = DateTime.UtcNow;
        var endtime = startTime.AddDays(7);
        
        var auction = Auction.Create(sellerId, title, description, startingPrice, startTime, endtime);
        await auctionRepo.AddAsync(auction);
        var auctions = await auctionRepo.GetAllAsync();
        Assert.NotEmpty(auctions); // TODO: Fix possible multiple enumeration
        
        await auctionRepo.DeleteAsync(auction.Id);
        Assert.Empty(auctions);
    }
}
