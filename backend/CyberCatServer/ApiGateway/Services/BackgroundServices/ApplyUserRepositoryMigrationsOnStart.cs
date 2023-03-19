using ApiGateway.Repositories;
using ApiGateway.Repositories.Migrations;

namespace ApiGateway.Services.BackgroundServices;

public class ApplyUserRepositoryMigrationsOnStart : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ApplyUserRepositoryMigrationsOnStart(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        await userRepository.ApplyMigration(new AddDevPlayers());
    }
}