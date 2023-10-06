using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using Shared.Server.Data;
using Shared.Server.Ids;

namespace AuthService.Tests.Mocks;

internal class MockAuthUserRepository : IAuthUserRepository
{
    public int Count => _users.Count;

    private readonly List<UserDbModel> _users = new();

    public Task<UserDto> FindByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(user => user.Email == email);
        return Task.FromResult(user?.ToDto());
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

    public Task SetJwtAuthenticationAccessTokenAsync(UserId userId, string accessToken)
    {
        Console.WriteLine($"Set access token '{accessToken}' for user '{userId}'");
        return Task.CompletedTask;
    }

    public Task<UserId> Create(string email, string password, string name)
    {
        var authUser = new UserDbModel(name, email, this)
        {
            PasswordHash = password.GetHashCode().ToString()
        };
        _users.Add(authUser);

        return Task.FromResult(new UserId(authUser.Id));
    }

    public Task Remove(UserId userId)
    {
        var findingUser = _users.FirstOrDefault(u => u.Id == userId.Value);
        _users.Remove(findingUser);

        return Task.CompletedTask;
    }
}