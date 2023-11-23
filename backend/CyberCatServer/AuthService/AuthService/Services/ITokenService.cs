using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;
using Shared.Models.Models.AuthorizationTokens;
using Shared.Server.Data;

namespace AuthService.Services;

public interface ITokenService
{
    AuthorizationToken CreateToken(User user);
}