using Shared.Models;

namespace AuthServiceService.Services;

public interface ITokenService
{
    string CreateToken(IUser user);
}