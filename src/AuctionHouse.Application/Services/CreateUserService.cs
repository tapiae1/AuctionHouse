using AuctionHouse.Domain.Exceptions;
using AuctionHouse.Domain.Entities; 
using AuctionHouse.Domain.Repositories;

namespace AuctionHouse.Application.Services;

public class CreateUserService
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(string username, string email, string passwordHash)
    {
        // Maybe add some account validation here? If email exists?
        // What if user already exists? 
        
        var user = User.Create(username, email, passwordHash);
        await _userRepository.AddAsync(user);

        return user; 
    }
}