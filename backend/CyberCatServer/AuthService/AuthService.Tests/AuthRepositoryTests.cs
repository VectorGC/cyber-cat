using AuthService.Tests.Mocks;
using AuthServiceService.Repositories;
using AuthServiceService.Repositories.InternalModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace AuthService.Tests;

// Проверяем, корректно ли создается и удаляются пользователи.
[TestFixture]
public class AuthRepositoryTests
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
        var user = new User
        {
            Email = "test@email.com",
            UserName = "Test User Name"
        };

        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();

        var notExistingUser = await userRepository.FindByEmailAsync(user.Email);
        Assert.Null(notExistingUser);

        await userRepository.Add(user, password);
        var createdUser = await userRepository.FindByEmailAsync(user.Email);
        Assert.NotNull(createdUser);

        await userRepository.Remove(createdUser);
        var removedUser = await userRepository.FindByEmailAsync(user.Email);
        Assert.Null(removedUser);
    }
}