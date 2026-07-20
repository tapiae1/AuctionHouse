using AuctionHouse.Domain.Enums;
using AuctionHouse.Domain.Exceptions;

namespace AuctionHouse.Domain.Entities;

public class Auction
{
// PROPERTIES
    public Guid Id { get; private set; } 
    public Guid SellerId { get; private set; }
    public string Title { get; private set; } = string.Empty; 
    public string Description { get; private set; } = string.Empty; 
    public AuctionStatus Status { get; private set; }
    public decimal StartingPrice { get; private set; } 
    public decimal CurrentBid { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

// PRIVATE CONSTRUCTOR 
    private Auction() {}

// CREATE METHOD
    public static Auction Create(Guid sellerId, string title, string description, decimal startingPrice,
            DateTime startTime, DateTime endTime)
    {
        return new Auction
        {
            Id = Guid.NewGuid(),
            SellerId = sellerId,
            Title = title, 
            Description = description,
            StartingPrice = startingPrice,
            CurrentBid = startingPrice,
            StartTime = startTime,  
            EndTime = endTime,
            Status = startTime > DateTime.UtcNow ? AuctionStatus.Scheduled : AuctionStatus.Active,
        }; 
    }

// PLACEBID METHOD
    public void PlaceBid(decimal amount)
    {
        // Check if auction is active
        if (Status != AuctionStatus.Active) 
            throw new DomainException("Auction is not active.");  

        // Check if the time is valid 
        if (DateTime.UtcNow > EndTime)
            throw new DomainException("Auction has ended"); 
         
        // Check if its more than current amount
        if (amount <= CurrentBid) 
            throw new DomainException("Bid must be higher than previous bid.");
        
        CurrentBid = amount;
    }









}
