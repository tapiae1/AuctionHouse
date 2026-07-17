namespace AuctionHouse.Domain.Entities;

public class Bid {
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public Guid BidderId { get; private set; } 
    public decimal Amount { get; private set; }
    public DateTime PlacedAt { get; private set; }

    private Bid() { }

    public static Bid Create(Guid id, Guid auctionId, Guid bidderId, decimal amount) {
        return new Bid{
            Id = Guid.NewGuid(),
            AuctionId = auctionId,
            BidderId = bidderId,
            Amount = amount, 
            PlacedAt = DateTime.UtcNow
        };
    }
}
