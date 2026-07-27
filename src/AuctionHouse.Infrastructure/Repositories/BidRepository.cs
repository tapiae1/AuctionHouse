using AuctionHouse.Domain.Entities;
using AuctionHouse.Domain.Repositories;

namespace AuctionHouse.Infrastructure.Repositories;

public class BidRepository : IBidRepository
{
    // In memory dictionary for database. 
    private readonly Dictionary<Guid, Bid> _bids = new(); 

    public Task<Bid?> GetByIdAsync(Guid id)
    {
        _bids.TryGetValue(id, out Bid? bid); 
        return Task.FromResult(bid);
    }

    public Task<IEnumerable<Bid>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Bid>>(_bids.Values); 
    }

    public Task<IEnumerable<Bid>> GetByBidderIdAsync(Guid id)
    {
        return Task.FromResult<IEnumerable<Bid>>(_bids.Values.Where(b => b.BidderId == id));
    }

    public Task AddAsync(Bid bid) 
    {
        _bids.Add(bid.Id, bid);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Bid bid)
    {
        _bids[bid.Id] = bid; 
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id) 
    {
        _bids.Remove(id); 
        return Task.CompletedTask; 
    }
}
