//Service depends on Domain-defined Interfaces, never on AuctionHouse.Infrastructure directly. 
using AuctionHouse.Domain.Entities; 
using AuctionHouse.Domain.Repositories;
using AuctionHouse.Domain.Exceptions;

namespace AuctionHouse.Application.Services; 


public class PlaceBidService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;
    
    // CONSTRUCTOR 
    public PlaceBidService(IAuctionRepository auctionRepository, IBidRepository bidRepository)
    {
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
    }

    // PlaceBid method 
    public async Task<Bid> PlaceBidAsync(Guid auctionId, Guid bidderId, decimal bidAmount)
    {
        // Get Auction 
        var auction = await _auctionRepository.GetByIdAsync(auctionId);
        if (auction == null)
        {
            throw new NotFoundException("Auction not found");// 
        }
       
        // Place the bid, then update auction repo
        auction.PlaceBid(bidAmount);  
        await _auctionRepository.UpdateAsync(auction); 
        
        // Build the bid, and add it to repo  
        var bid = Bid.Create(auctionId, bidderId, bidAmount);
        await _bidRepository.AddAsync(bid); 
        return bid;
    }
}