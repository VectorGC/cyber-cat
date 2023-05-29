using Shared.Models;

namespace AuthService.Service.Services;

public interface ITokenService
{
    string CreateToken(IUser user);
}