using AuthService.Service.Repositories;
using AuthService.Service.Repositories.InternalModels;
using Shared.Models;

namespace AuthServiceTests.Mocks;

internal class MockAuthUserRepository : IAuthUserRepository
{
    private readonly List<User> _users = new();

    public Task<IUser?> FindByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(user => user.Email == email);
        return Task.FromResult<IUser?>(user);
    }

    public Task<bool> CheckPasswordAsync(IUser user, string password)
    {
        var findingUser = _users.FirstOrDefault(u => u.Email == user.Email);

        return Task.FromResult(findingUser.PasswordHash == password.GetHashCode().ToString());
    }

    public Task SetJwtAuthenticationAccessTokenAsync(IUser user, string? accessToken)
    {
        Console.WriteLine($"Set access token '{accessToken}' for user '{user.Email}'");
        return Task.CompletedTask;
    }

    public Task Add(IUser user, string password)
    {
        var authUser = new User(user)
        {
            PasswordHash = password.GetHashCode().ToString()
        };
        _users.Add(authUser);

        return Task.CompletedTask;
    }

    public Task Remove(IUser user)
    {
        var findingUser = _users.FirstOrDefault(u => u.Email == user.Email);
        _users.Remove(findingUser);

        return Task.CompletedTask;
    }
}