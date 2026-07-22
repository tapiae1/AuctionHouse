using AuctionHouse.Domain.Entities; 
using AuctionHouse.Domain.Repositories; 

namespace AuctionHouse.Infrastructure.Repositories; 

public class UserRepository : IUserRepository
{
    // Create a dictionary in place for a real database. 
    private readonly Dictionary<Guid, User> _users = new(); 

    public Task<User?> GetByIdAsync(Guid id) 
    {
        _users.TryGetValue(id, out User? user); 
        return Task.FromResult(user); 
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users.Values);
    }

    public Task AddAsync(User user) 
    {
        _users.Add(user.Id, user); 
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        _users[user.Id] = user; 
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id) 
    {
        _users.Remove(id); 
        return Task.CompletedTask; 
    }
}
