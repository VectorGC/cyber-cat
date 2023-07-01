using Shared.Models;
using Shared.Models.Models;

namespace AuthService.Repositories;

public interface IAuthUserRepository
{
    Task Add(IUser user, string password);
    Task Remove(string email);
    Task<IUser> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(string email, string password);
    Task SetJwtAuthenticationAccessTokenAsync(string email, string accessToken);
}