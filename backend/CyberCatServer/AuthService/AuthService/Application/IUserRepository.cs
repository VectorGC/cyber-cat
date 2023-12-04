using System.Threading.Tasks;
using AuthService.Domain;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Application;

public enum UserRepositoryError
{
    None = 0,
    Unknown = 1,
    DuplicateEmail,
    InvalidUserNameCharacters,
}

public interface IUserRepository
{
    Task<CreateUserResult> CreateUser(string email, string password, string name);
    Task<DeleteUserResult> DeleteUser(UserId id);
    Task<UpdateUserResult> UpdateUser(UserEntity user);
    Task<UserEntity> GetUser(UserId userId);
    Task<UserEntity> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(UserEntity user, string password);
    Task SetAuthenticationTokenAsync(UserEntity user, AuthorizationToken token);
    Task<string> GetRoleId(string roleId);
    Task<bool> RoleExists(string roleId);
    Task<CreateRoleResult> CreateRole(string roleId);
    Task<int> GetUsersCountWithRole(string roleId);
}

public record CreateUserResult(bool Success, UserRepositoryError Error, UserEntity CreatedUser);

public record DeleteUserResult(bool Success, string Error, UserEntity RemovableUser);

public record UpdateUserResult(bool Success, UserRepositoryError Error);

public record CreateRoleResult(bool Success, string Error);