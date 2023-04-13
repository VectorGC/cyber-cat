using CompilerServiceAPI.Services;

namespace CompilerServiceAPI;

public static class ServicesSetup
{
    public static IServiceCollection AddCompilationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandService, CommandService>();

        return serviceCollection;
    }
}