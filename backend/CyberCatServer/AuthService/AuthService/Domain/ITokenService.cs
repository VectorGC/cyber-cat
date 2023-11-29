using AuthService.Domain.Models;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Domain;

public interface ITokenService
{
    AuthorizationToken CreateToken(UserEntity user);
}