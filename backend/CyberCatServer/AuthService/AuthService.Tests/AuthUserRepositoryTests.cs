using AuthService.Domain;
using AuthService.Tests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Domain.Users;
using Shared.Tests;

namespace AuthService.Tests;

[TestFixture]
public class AuthUserRepositoryTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>().AddScoped<Program, IUserRepository, MockUserRepository>();
    }

    [Test]
    public async Task ShouldCreateAndRemoveUser_WhenPassValidParameters()
    {
        var password = "123";
        var email = "test@email.com";
        var userName = "Test User Name";

        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var notExistingUser = await userRepository.FindByEmailAsync(email);
        Assert.Null(notExistingUser);

        var resultCreated = await userRepository.CreateUser(email, password, userName);
        Assert.IsTrue(resultCreated.Success);
        var user = resultCreated.CreatedUser;
        Assert.NotNull(user);

        var createdUser = await userRepository.FindByEmailAsync(email);
        Assert.NotNull(createdUser);

        var resultRemovable = await userRepository.RemoveUser(new UserId(user.Id));
        Assert.IsTrue(resultRemovable.Success);
        var removedUser = await userRepository.FindByEmailAsync(email);
        Assert.Null(removedUser);
    }
}