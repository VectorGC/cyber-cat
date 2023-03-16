using ApiGateway.Models;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public interface IAuthUserService
{
    Task<string?> Authenticate(string email, string password);
}

public class AuthUserService : IAuthUserService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly IUserService _userService;

    public AuthUserService(IAuthUserRepository authUserRepository, IUserService userService)
    {
        _authUserRepository = authUserRepository;
        _userService = userService;
    }

    public async Task<string?> Authenticate(string email, string password)
    {
        var user = await _userService.GetUserByEmail(email);
        if (user.Password != password)
        {
            throw new UnauthorizedAccessException("Incorrect password");
        }

        var token = await GetOrCreateToken(user);
        return token;
    }

    private async Task<string?> GetOrCreateToken(IUser user)
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