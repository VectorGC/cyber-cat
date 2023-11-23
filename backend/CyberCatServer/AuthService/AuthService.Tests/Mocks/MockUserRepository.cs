using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace AuthService.Tests.Mocks;

internal class MockUserRepository : IUserRepository
{
    private readonly List<UserDbModel> _users = new();

    public Task<User> FindByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(user => user.Email == email);
        return Task.FromResult(user?.ToDomainModel());
    }

    public Task<bool> CheckPasswordAsync(UserId userId, string password)
    {
        var findingUser = _users.FirstOrDefault(u => u.Id == userId.Value);
        return Task.FromResult(findingUser.PasswordHash == password.GetHashCode().ToString());
    }

    public Task<bool> Contains(UserId userId)
    {
        var contains = _users.Count(user => user.Id == userId.Value) > 0;
        return Task.FromResult(contains);
    }

    public Task SetAuthenticationTokenAsync(UserId id, AuthorizationToken token)
    {
        Console.WriteLine($"Set access token '{token.Value}' for user '{id}'");
        return Task.CompletedTask;
    }

    public Task<User> CreateUser(string email, string password, string name)
    {
        var authUser = new UserDbModel(name, email, this)
        {
            PasswordHash = password.GetHashCode().ToString()
        };
        _users.Add(authUser);

        return Task.FromResult(authUser.ToDomainModel());
    }

    public Task Remove(UserId userId)
    {
        var findingUser = _users.FirstOrDefault(u => u.Id == userId.Value);
        _users.Remove(findingUser);

        return Task.CompletedTask;
    }

    public Task<int> GetUsersCountWithRole(Role role)
    {
        return Task.FromResult(1);
    }

    public Task<User> GetUser(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<SaveUserResult> SaveUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleId(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RoleExists(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<AddRoleResult> CreateRole(Role role)
    {
        throw new NotImplementedException();
    }
}