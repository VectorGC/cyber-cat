using AuthService.Repositories;
using AuthService.Repositories.InternalModels;

namespace AuthService.Services;

public class AddCyberCatUserToRepositoryOnStart : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AddCyberCatUserToRepositoryOnStart> _logger;

    public AddCyberCatUserToRepositoryOnStart(IServiceProvider serviceProvider, ILogger<AddCyberCatUserToRepositoryOnStart> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempt add default user");
        var user = new User
        {
            UserName = "Cat",
            Email = "cat"
        };

        await using var scope = _serviceProvider.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();

        var exists = await repository.FindByEmailAsync(user.Email) != null;
        if (exists)
        {
            _logger.LogInformation("Default user already added");
            return;
        }

        await repository.Add(user, "cat");

        _logger.LogInformation("Default user has been added");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop auto load tasks");
        return Task.CompletedTask;
    }
}