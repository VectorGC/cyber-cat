using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Application;
using AuthService.Infrastructure;
using AuthService.Tests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Configurations;
using Shared.Server.Services;
using Shared.Tests;

namespace AuthService.Tests;

[TestFixture]
public class AuthenticationAndPlayerAuthorization
{
    private WebApplicationFactory<Program> _factory;
    private readonly MockUserRepository _mockUserRepository = new();
    private const string UserPassword = "123";
    private const string Email = "test@email.com";
    private const string UserName = "Test User Name";

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>().AddScoped<Program, IUserRepository>(_mockUserRepository);

        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        userRepository.CreateUser(Email, UserPassword, UserName);
    }

    [Test]
    public async Task ShouldGetToken_WhenPassValidUserCredentials()
    {
        using var channel = _factory.CreateGrpcChannel();
        var authenticationService = channel.CreateGrpcService<IAuthService>();

        var args = new GetAccessTokenArgs(Email, UserPassword);
        var response = await authenticationService.GetAccessToken(args);
        var token = ((AuthorizationToken) response).Value;

        Assert.IsNotEmpty(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler.ValidateToken(token, JwtTokenValidation.TokenValidationParameters, out _);

        Assert.AreEqual(Email, claims.FindFirst(ClaimTypes.Email)!.Value);
        Assert.AreEqual(UserName, claims.FindFirst(ClaimTypes.Name)!.Value);
    }
}