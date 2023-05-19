using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using AuthServiceTests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Configurations;
using Shared.Dto.Args;
using Shared.Models;
using Shared.Services;

namespace AuthServiceTests;

[TestFixture]
public class AuthServiceTests
{
    private WebApplicationFactory<Program> _factory;
    private readonly MockAuthUserRepository _mockAuthUserRepository = new();
    private const string UserPassword = "123";

    private readonly IUser _user = new User
    {
        Email = "test@email.com",
        UserName = "Test User Name"
    };

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>().AddScoped<Program, IAuthUserRepository>(_mockAuthUserRepository);

        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();
        userRepository.Add(_user, UserPassword);
    }

    [Test]
    public async Task ShouldGetToken_WhenPassValidUserCredentials()
    {
        using var channel = _factory.CreateGrpcChannel();
        var authenticationService = channel.CreateGrpcService<IAuthGrpcService>();

        var args = new GetAccessTokenArgs
        {
            Email = _user.Email,
            Password = UserPassword
        };
        var token = await authenticationService.GetAccessToken(args);

        Assert.IsNotEmpty(token.AccessToken);

        var parameters = JwtTokenValidation.CreateTokenParameters();
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = tokenHandler.ValidateToken(token.AccessToken, parameters, out _);

        Assert.AreEqual(_user.Email, claims.FindFirst(ClaimTypes.Email)!.Value);
        Assert.AreEqual(_user.UserName, claims.FindFirst(ClaimTypes.Name)!.Value);
    }
}