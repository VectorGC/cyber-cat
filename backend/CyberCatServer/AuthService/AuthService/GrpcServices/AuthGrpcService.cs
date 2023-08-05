using AuthService.Repositories;
using AuthService.Services;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Server.Exceptions;
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