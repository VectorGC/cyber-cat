using System.Text.Json;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;

namespace AuthService.Services;

public class AddCyberCatUserToRepositoryOnStart : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<AddCyberCatUserToRepositoryOnStart> _logger;

    public AddCyberCatUserToRepositoryOnStart(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment, ILogger<AddCyberCatUserToRepositoryOnStart> logger)
    {
        _serviceProvider = serviceProvider;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempt add default user");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();

        var exists = await repository.FindByEmailAsync("cyber@cyber.cat") != null;
        if (exists)
        {
            _logger.LogInformation("Default user already added");
            return;
        }

        var user = new User
        {
            UserName = "CyberCat",
            Email = "cyber@cat"
        };

        await repository.Add(user, "Cyber_Cat123@");

        _logger.LogInformation("Default user has been added");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop auto load tasks");
        return Task.CompletedTask;
    }
}