using ApiGateway.Repositories;
using ApiGateway.Services;
using ApiGateway.Services.BackgroundServices;

namespace ApiGateway;

public static class ServicesSetup
{
    public static IServiceCollection AddUserService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepositoryMongoDb>();
        serviceCollection.AddHostedService<ApplyUserRepositoryMigrationsOnStart>();

        return serviceCollection;
    }

    public static IServiceCollection AddAuthUserService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
        serviceCollection.AddScoped<IAuthUserRepository, AuthUserRepositoryMongoDb>();
        return serviceCollection;
    }
}