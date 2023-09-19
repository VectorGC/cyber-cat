using System;
using System.Threading;
using System.Threading.Tasks;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

        await using var scope = _serviceProvider.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();
        var user = new UserDbModel("cat", "cat", repository);

        var exists = await repository.FindByEmailAsync(user.Email) != null;
        if (exists)
        {
            _logger.LogInformation("Default user already added");
            return;
        }

        await repository.Create(user.Email, "cat", user.UserName);

        _logger.LogInformation("Default user has been added");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop auto load tasks");
        return Task.CompletedTask;
    }
}