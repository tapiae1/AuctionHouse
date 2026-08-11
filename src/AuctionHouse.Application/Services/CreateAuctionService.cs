using AuctionHouse.Domain.Entities;
using AuctionHouse.Domain.Exceptions;
using AuctionHouse.Domain.Repositories;

namespace AuctionHouse.Application.Services;

public class CreateAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IUserRepository _userRepository;// TODO: Add user validation check when creating an auction. 

    public CreateAuctionService(IAuctionRepository auctionRepository, IUserRepository userRepository)
    {
        _auctionRepository = auctionRepository;
        _userRepository = userRepository;
    }


    public async Task<Auction> CreateAuctionAsync(Guid sellerId, string title, string description, decimal startingPrice, DateTime startTime, DateTime endTime)
    { 
        // Check to see if the seller exists
        var seller = await _userRepository.GetByIdAsync(sellerId);
        if (seller == null)
        {
            throw new NotFoundException("User not found"); // 
        }
        
        // Create auction and Add to repo
        var auction = Auction.Create(sellerId, title, description, startingPrice, startTime, endTime); 
        await _auctionRepository.AddAsync(auction); // TODO: what if there's already an auction? 
        
        return auction; 
    }
}