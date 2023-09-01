using System.Threading.Tasks;
using AuthService.Repositories;
using AuthService.Services;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Exceptions.AuthService;
using Shared.Server.Models;
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
        try
        {
            return await _authUserRepository.Create(args.Email, args.Password, args.Name);
        }
        catch (IdentityUserException ex)
        {
            return ex;
        }
    }

    public async Task<Response<UserId>> FindByEmail(StringProto email)
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

    public async Task Remove(UserId id)
    {
        await _authUserRepository.Remove(id);
    }
}