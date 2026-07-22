using AuctionHouse.Domain.Entities; 
using AuctionHouse.Domain.Repositories; 

namespace AuctionHouse.Infrastructure.Repositories;

public class AuctionRepository : IAuctionRepository
{
    // Create a dictionary that will stand in place for a real database. 
    private readonly Dictionary<Guid, Auction> _auctions = new();     

    public Task<Auction?> GetByIdAsync(Guid id)
    {
        _auctions.TryGetValue(id, out Auction? auction); 
        return Task.FromResult(auction); 
    }

    public Task<IEnumerable<Auction>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Auction>>(_auctions.Values);
    }

    public Task<IEnumerable<Auction>> GetBySellerIdAsync(Guid id) 
    {
        return Task.FromResult<IEnumerable<Auction>>(_auctions.Values.Where(a => a.SellerId == id)); 
    }

    public Task AddAsync(Auction auction)
    {
        _auctions.Add(auction.Id, auction);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Auction auction)
    {
        _auctions[auction.Id] = auction; 
        return Task.CompletedTask; 
    }

    public Task DeleteAsync(Guid id)
    {
        _auctions.Remove(id);
        return Task.CompletedTask;
    }
}