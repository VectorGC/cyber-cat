using System.Threading.Tasks;
using AuthService.Domain.Models;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Domain;

public interface IUserRepository
{
    Task<CreateUserResult> CreateUser(string email, string password, string name);
    Task<RemoveUserResult> RemoveUser(UserId id);
    Task<SaveUserResult> SaveUser(UserModel user);
    Task<UserModel> GetUser(UserId userId);
    Task<UserModel> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(UserModel user, string password);
    Task SetAuthenticationTokenAsync(UserModel user, AuthorizationToken token);
    Task<string> GetRoleId(Role role);
    Task<bool> RoleExists(Role role);
    Task<CreateRoleResult> CreateRole(Role role);
    Task<int> GetUsersCountWithRole(Role role);
}

public record CreateUserResult(bool Success, string Error, UserModel CreatedUser);
public record RemoveUserResult(bool Success, string Error, UserModel RemovableUser);
public record SaveUserResult(bool Success, string Error);
public record CreateRoleResult(bool Success, string Error);