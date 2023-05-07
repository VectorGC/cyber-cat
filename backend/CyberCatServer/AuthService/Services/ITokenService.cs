using AuthService.Models;

namespace AuthService.Services;

public interface ITokenService
{
    string CreateToken(IUser user);
}