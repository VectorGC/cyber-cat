using Shared.Models;

namespace AuthServiceService.Repositories;

public interface IAuthUserRepository
{
    Task Add(IUser user, string password);
    Task Remove(IUser user);
    Task<IUser?> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(IUser user, string password);
    Task SetJwtAuthenticationAccessTokenAsync(IUser user, string? accessToken);
}