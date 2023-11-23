using System.Threading.Tasks;
using AuthService.Repositories;
using AuthService.Services;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Data;
using Shared.Server.Ids;
using Shared.Server.Infrastructure.Exceptions;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace AuthService.GrpcServices;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Response<UserId>> CreateUser(CreateUserArgs args)
    {
        var (email, password, userName) = args;
        try
        {
            var user = await _userRepository.CreateUser(email, password, userName);
            return user.Id;
        }
        catch (IdentityException ex)
        {
            return ex;
        }
    }

    public async Task<Response<UserId>> FindByEmail(Args<string> email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        return user?.Id;
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

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user.Id, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        var token = _tokenService.CreateToken(user);
        await _userRepository.SetAuthenticationTokenAsync(user.Id, token);

        return token;
    }

    public async Task<Response> Remove(RemoveArgs args)
    {
        var (userId, password) = args;
        var isPasswordValid = await _userRepository.CheckPasswordAsync(userId, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        await _userRepository.Remove(userId);
        return new Response();
    }

    public async Task<Response> RemoveDev(RemoveDevArgs args)
    {
        var user = await _userRepository.FindByEmailAsync(args.Email);
        if (user != null)
        {
            await _userRepository.Remove(user.Id);
        }

        return new Response();
    }
}