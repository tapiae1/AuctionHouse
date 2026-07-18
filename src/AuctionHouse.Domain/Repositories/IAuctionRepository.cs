using AuctionHouse.Domain.Entities;

namespace AuctionHouse.Domain.Repositories;

public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Auction>> GetAllAsync(); 
    Task<IEnumerable<Auction>> GetBySellerIdAsync(Guid id); 
    Task AddAsync(Auction auction); 
    Task UpdateAsync(Auction auction); 
    Task DeleteAsync(Guid id); 
}
