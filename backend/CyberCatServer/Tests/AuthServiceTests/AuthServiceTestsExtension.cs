using AuthService.Repositories;
using AuthServiceTests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Shared;

namespace AuthServiceTests;

public static class AuthServiceTestsExtension
{
    public static WebApplicationFactory<Program> GetAuthServiceWebFactoryWithMocks()
    {
        return new WebApplicationFactory<Program>().AddScoped<Program, IAuthUserRepository, MockAuthUserRepository>();
    }
}