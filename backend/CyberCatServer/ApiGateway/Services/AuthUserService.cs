using ApiGateway.Models;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public interface IAuthUserService
{
    Task<string> Authenticate(string email, string password);
    Task<UserId> Authorize(string token);
}

public class AuthUserService : IAuthUserService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public AuthUserService(IAuthUserRepository authUserRepository, IUserRepository userRepository, ILogger<AuthUserService> logger)
    {
        _authUserRepository = authUserRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<string> Authenticate(string email, string password)
    {
        var userId = await _userRepository.FindByEmailSlowly(email);
        var user = await _userRepository.GetUser(userId);
        if (user.Password != password)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        return await GetOrCreateToken(user);
    }

    public async Task<UserId> Authorize(string token)
    {
        return await _userRepository.FindByTokenSlowly(token);
    }

    private async Task<string> GetOrCreateToken(IUser user)
    {
        var token = await _authUserRepository.GetTokenOrEmpty(user);
        if (string.IsNullOrEmpty(token))
        {
            _logger.LogInformation("Create token for user '{User}'", user);
            return await _authUserRepository.CreateToken(user);
        }

        _logger.LogInformation("Get token for user '{User}'", user);
        return token;
    }
}