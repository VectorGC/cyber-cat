using System.Threading.Tasks;
using AuthService.Repositories;
using AuthService.Services;
using Shared.Server.Data;
using Shared.Server.Exceptions.AuthService;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace AuthService.GrpcServices;

public class AuthGrpcService : IAuthGrpcService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly ITokenService _tokenService;

    public AuthGrpcService(IAuthUserRepository authUserRepository, ITokenService tokenService)
    {
        _authUserRepository = authUserRepository;
        _tokenService = tokenService;
    }

    public async Task<Response<UserId>> CreateUser(CreateUserArgs args)
    {
        var (email, password, userName) = args;
        try
        {
            var user = await _authUserRepository.Create(email, password, userName);
            return user.Id;
        }
        catch (IdentityUserException ex)
        {
            return ex;
        }
    }

    public async Task<Response<UserId>> FindByEmail(Args<string> email)
    {
        var user = await _authUserRepository.FindByEmailAsync(email);
        return user?.Id;
    }

    public async Task<Response<string>> GetAccessToken(GetAccessTokenArgs args)
    {
        var email = args.Email;
        var password = args.Password;

        var user = await _authUserRepository.FindByEmailAsync(email);
        if (user == null)
        {
            return new UserNotFoundException(email);
        }

        var isPasswordValid = await _authUserRepository.CheckPasswordAsync(user.Id, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        var accessToken = _tokenService.CreateToken(user.Email, user.UserName);
        await _authUserRepository.SetJwtAuthenticationAccessTokenAsync(user.Id, accessToken);

        return accessToken;
    }

    public async Task<Response> Remove(RemoveArgs args)
    {
        var (userId, password) = args;
        var isPasswordValid = await _authUserRepository.CheckPasswordAsync(userId, password);
        if (!isPasswordValid)
        {
            return new UnauthorizedException("Invalid password");
        }

        await _authUserRepository.Remove(userId);
        return new Response();
    }

    public async Task<Response> RemoveDev(RemoveDevArgs args)
    {
        var user = await _authUserRepository.FindByEmailAsync(args.Email);
        if (user != null)
        {
            await _authUserRepository.Remove(user.Id);
        }

        return new Response();
    }

    public async Task<Response<string>> SignInWithVk(OAuthSignIn args)
    {
        var email = args.Email;
        var userName = args.UserName;
        var password = $"{email}_vk";

        var user = await _authUserRepository.FindByEmailAsync(email)
                   ?? await _authUserRepository.Create(email, password, userName);

        var token = await GetAccessToken(new GetAccessTokenArgs(user.Email, password));
        return token;
    }
}