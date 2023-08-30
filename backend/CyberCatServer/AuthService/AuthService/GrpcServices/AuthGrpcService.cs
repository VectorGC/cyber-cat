using System;
using System.Threading.Tasks;
using AuthService.Repositories;
using AuthService.Services;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto;
using Shared.Server.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Models;
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

    public async Task<UserId> CreateUser(CreateUserArgs args)
    {
        return await _authUserRepository.Create(args.Email, args.Password, args.Name);
    }

    public async Task<FindUserByEmailResponse> FindByEmail(StringProto email)
    {
        var user = await _authUserRepository.FindByEmailAsync(email);
        return new FindUserByEmailResponse()
        {
            UserId = user?.Id
        };
    }

    public async Task<StringProto> GetAccessToken(GetAccessTokenArgs args)
    {
        var email = args.Email;
        var password = args.Password;

        var user = await _authUserRepository.FindByEmailAsync(email);
        if (user == null)
        {
            throw new UserNotFound(email);
        }

        var isPasswordValid = await _authUserRepository.CheckPasswordAsync(user.Id, password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
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