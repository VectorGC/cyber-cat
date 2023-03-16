using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public interface IUserService
{
    Task<IUser> GetUserByEmail(string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IUser> GetUserByEmail(string email)
    {
        var user = await _userRepository.FindByEmailSlowly(email);
        if (user == null)
        {
            throw new UserNotFound();
        }

        return user;
    }
}