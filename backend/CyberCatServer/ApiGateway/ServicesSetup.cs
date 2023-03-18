using ApiGateway.Repositories;
using ApiGateway.Services;
using ApiGateway.Services.BackgroundServices;

namespace ApiGateway;

public static class ServicesSetup
{
    public static IServiceCollection AddUserServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepositoryMongoDb>();
        serviceCollection.AddHostedService<ApplyUserRepositoryMigrationsOnStart>();

        return serviceCollection;
    }

    public static IServiceCollection AddAuthUserServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthUserService, AuthUserService>();
        serviceCollection.AddScoped<IAuthUserRepository, AuthUserRepositoryMongoDb>();
        return serviceCollection;
    }

    public static IServiceCollection AddTaskServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITaskRepository, TasksHierarchyRepository>();
        serviceCollection.AddScoped<ITaskRepository, TasksFlatRepository>();

        serviceCollection.AddScoped<ITaskService, TaskService>();
        return serviceCollection;
    }
}