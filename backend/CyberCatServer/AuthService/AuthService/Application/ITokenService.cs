using AuthService.Domain;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Application;

public interface ITokenService
{
    AuthorizationToken CreateToken(UserEntity user);
}