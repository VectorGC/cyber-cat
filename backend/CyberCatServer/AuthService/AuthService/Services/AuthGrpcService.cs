using AuthService.Repositories;
using Shared.Dto;
using Shared.Dto.Args;
using Shared.Dto.ProtoHelpers;
using Shared.Exceptions;
using Shared.Services;

namespace AuthService.Services;

public class AuthGrpcService : IAuthGrpcService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly ITokenService _tokenService;

    public AuthGrpcService(IAuthUserRepository authUserRepository, ITokenService tokenService)
    {
        _authUserRepository = authUserRepository;
        _tokenService = tokenService;
    }

    public async Task CreateUser(CreateUserArgs args)
    {
        await _authUserRepository.Add(args.User, args.Password);
    }

    public async Task RemoveUser(StringProto email)
    {
        await _authUserRepository.Remove(email);
    }

    public async Task<TokenDto> GetAccessToken(GetAccessTokenArgs args)
    {
        var email = args.Email;
        var password = args.Password;

        var user = await _authUserRepository.FindByEmailAsync(email);
        if (user == null)
        {
            throw UserNotFound.NotFoundEmail(email);
        }

        var isPasswordValid = await _authUserRepository.CheckPasswordAsync(email, password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        var accessToken = _tokenService.CreateToken(user);
        await _authUserRepository.SetJwtAuthenticationAccessTokenAsync(email, accessToken);

        return new TokenDto {AccessToken = accessToken};
    }
}