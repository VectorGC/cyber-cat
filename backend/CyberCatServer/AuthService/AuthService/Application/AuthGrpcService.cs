using System.Threading.Tasks;
using AuthService.Domain;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Infrastructure.Exceptions;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace AuthService.Application;

public class AuthGrpcService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthGrpcService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Response<UserId>> CreateUser(CreateUserArgs args)
    {
        var (email, password, userName) = args;
        var result = await _userRepository.CreateUser(email, password, userName);
        if (!result.Success)
        {
            return new IdentityException(result.Error);
        }

        return new UserId(result.CreatedUser.Id);
    }

    public async Task<Response> RemoveUser(RemoveUserArgs args)
    {
        var (userId, password) = args;
        var user = await _userRepository.GetUser(userId);
        if (user == null)
            return new UserNotFoundException($"User '{userId}' not found");

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        var result = await _userRepository.RemoveUser(userId);
        if (!result.Success)
        {
            return new IdentityException(result.Error);
        }

        return new Response();
    }

    public async Task<Response<UserId>> FindByEmail(Args<string> email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
            return null;
        return new UserId(user.Id);
    }

    public async Task<Response<AuthorizationToken>> GetAccessToken(GetAccessTokenArgs args)
    {
        var email = args.Email;
        var password = args.Password;

        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
        {
            return new UserNotFoundException(email);
        }

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        var token = _tokenService.CreateToken(user);
        await _userRepository.SetAuthenticationTokenAsync(user, token);

        return token;
    }
}