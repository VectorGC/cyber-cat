using System.Threading.Tasks;
using Shared.Server.Data;
using Shared.Server.Ids;

namespace AuthService.Repositories;

public interface IAuthUserRepository
{
    int Count { get; }
    Task<UserDto> Create(string email, string password, string name);
    Task Remove(UserId id);
    Task<UserDto> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(UserId id, string password);
    Task SetJwtAuthenticationAccessTokenAsync(UserId id, string accessToken);
}