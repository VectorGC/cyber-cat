using Shared.Models;
using Shared.Models.Models;

namespace AuthService.Services;

public interface ITokenService
{
    string CreateToken(IUser user);
}