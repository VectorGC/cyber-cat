using System.Threading.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Repositories;

public interface IUserRepository
{
    Task<User> CreateUser(string email, string password, string name);
    Task Remove(UserId id);
    Task<User> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(UserId id, string password);
    Task SetAuthenticationTokenAsync(UserId id, AuthorizationToken token);
    Task<User> GetUser(UserId userId);
    Task<SaveUserResult> SaveUser(User user);
    Task<string> GetRoleId(Role role);
    Task<bool> RoleExists(Role role);
    Task<AddRoleResult> CreateRole(Role role);
    Task<int> GetUsersCountWithRole(Role role);
}

public record SaveUserResult(bool Success, string Error);
public record AddRoleResult(bool Success, string Error);
