using AuctionHouse.Domain.Entities; 

namespace AuctionHouse.Domain.Repositories;

public interface IUserRepository 
{
    //Task<User> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(Guid id); 
    Task<IEnumerable<User>> GetAllAsync(); 
    Task AddAsync(User user);
    Task UpdateAsync(User user); 
    Task DeleteAsync(Guid id); 
}