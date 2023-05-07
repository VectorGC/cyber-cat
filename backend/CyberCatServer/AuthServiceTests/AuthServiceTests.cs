using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Controllers;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Configurations;
using Shared.Dto;

namespace AuthServiceTests;

[TestFixture]
public class AuthServiceTests
{
    private WebApplicationFactory<Program> _factory;
    private readonly MockAuthUserRepository _mockAuthUserRepository = new();
    private readonly string _userPassword = "123";

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
        userRepository.Add(_user, _userPassword);
    }

    [Test]
    public async Task ShouldGetToken_WhenPassValidUserCredentials()
    {
        using var channel = _factory.CreateGrpcChannel();
        var authenticationService = channel.CreateGrpcService<IAuthenticationService>();

        var args = new GetAccessTokenArgsDto
        {
            Email = _user.Email,
            Password = _userPassword
        };
        var token = await authenticationService.GetAccessToken(args);

        Assert.IsNotEmpty(token.Value);

        var parameters = JwtTokenValidation.CreateTokenParameters();
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = tokenHandler.ValidateToken(token.Value, parameters, out _);

        Assert.AreEqual(_user.Email, claims.FindFirst(ClaimTypes.Email)!.Value);
        Assert.AreEqual(_user.UserName, claims.FindFirst(ClaimTypes.Name)!.Value);
    }
}