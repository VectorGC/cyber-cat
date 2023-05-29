using AuthServiceService.Repositories;
using AuthServiceService.Repositories.InternalModels;
using Shared.Models;

namespace AuthService.Tests.Mocks;

internal class MockAuthUserRepository : IAuthUserRepository
{
    private readonly List<User> _users = new();

    public Task<IUser?> FindByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(user => user.Email == email);
        return Task.FromResult<IUser?>(user);
    }

    public Task<bool> CheckPasswordAsync(string email, string password)
    {
        var findingUser = _users.FirstOrDefault(u => u.Email == email);

        return Task.FromResult(findingUser.PasswordHash == password.GetHashCode().ToString());
    }

    public Task SetJwtAuthenticationAccessTokenAsync(string email, string? accessToken)
    {
        Console.WriteLine($"Set access token '{accessToken}' for user '{email}'");
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

    public Task Remove(string email)
    {
        var findingUser = _users.FirstOrDefault(u => u.Email == email);
        _users.Remove(findingUser);

        return Task.CompletedTask;
    }
}