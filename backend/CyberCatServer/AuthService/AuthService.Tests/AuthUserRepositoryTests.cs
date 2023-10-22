using AuthService.Repositories;
using AuthService.Tests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Tests;

namespace AuthService.Tests;

[TestFixture]
public class AuthUserRepositoryTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>().AddScoped<Program, IAuthUserRepository, MockAuthUserRepository>();
    }

    [Test]
    public async Task ShouldCreateAndRemoveUser_WhenPassValidParameters()
    {
        var password = "123";
        var email = "test@email.com";
        var userName = "Test User Name";

        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();

        var notExistingUser = await userRepository.FindByEmailAsync(email);
        Assert.Null(notExistingUser);

        var user = await userRepository.Create(email, password, userName);
        Assert.NotNull(user);

        var createdUser = await userRepository.FindByEmailAsync(email);
        Assert.NotNull(createdUser);

        await userRepository.Remove(user.Id);
        var removedUser = await userRepository.FindByEmailAsync(email);
        Assert.Null(removedUser);
    }
}