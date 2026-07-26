using AuctionHouse.Domain.Entities;

namespace AuctionHouse.Domain.Repositories;

public interface IBidRepository
{
    Task<Bid?> GetByIdAsync(Guid id);
    Task<IEnumerable<Bid>> GetAllAsync(); 
    Task<IEnumerable<Bid>> GetByBidderIdAsync(Guid id);
    Task AddAsync(Bid bid); 
    Task UpdateAsync(Bid bid);
    Task DeleteAsync(Guid id); 
}