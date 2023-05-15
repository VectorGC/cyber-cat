using AuthService.Repositories;
using Shared.Dto;
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

    public async Task<TokenResponseDto> GetAccessToken(GetAccessTokenArgsDto argsDto)
    {
        var email = argsDto.Email;
        var password = argsDto.Password;

        var user = await _authUserRepository.FindByEmailAsync(email);
        if (user == null)
        {
            throw UserNotFound.NotFoundEmail(email);
        }

        var isPasswordValid = await _authUserRepository.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        var accessToken = _tokenService.CreateToken(user);
        await _authUserRepository.SetJwtAuthenticationAccessTokenAsync(user, accessToken);

        return new TokenResponseDto {AccessToken = accessToken};
    }
}