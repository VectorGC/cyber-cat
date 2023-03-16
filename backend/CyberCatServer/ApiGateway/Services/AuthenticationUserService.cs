using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public interface IAuthenticationUserService
{
    Task<string> Authenticate(string email, string password);
    Task<IUser> Authorize(string? token);
}

public class AuthenticationUserService : IAuthenticationUserService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly IUserRepository _userRepository;

    public AuthenticationUserService(IAuthUserRepository authUserRepository, IUserRepository userRepository)
    {
        _authUserRepository = authUserRepository;
        _userRepository = userRepository;
    }

    public async Task<IUser> Authorize(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException(nameof(token));
        }

        var user = await _userRepository.FindByTokenSlowly(token);
        if (user == null)
        {
            throw new UserNotFound();
        }

        return user;
    }

    public async Task<string> Authenticate(string email, string password)
    {
        var user = await _userRepository.FindByEmailSlowly(email);
        if (user.Password != password)
        {
            throw new UnauthorizedAccessException("Incorrect password");
        }

        var token = await GetOrCreateToken(user);
        return token;
    }

    private async Task<string> GetOrCreateToken(IUser user)
    {
        var token = await _authUserRepository.GetTokenOrEmpty(user);
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"Create token for user '{user}'");
            return await _authUserRepository.CreateToken(user);
        }

        Console.WriteLine($"Get token for user '{user}'");
        return token;
    }
}