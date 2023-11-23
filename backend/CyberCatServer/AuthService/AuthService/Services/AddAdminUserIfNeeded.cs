using System;
using System.Threading;
using System.Threading.Tasks;
using AuthService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models.Domain.Users;

namespace AuthService.Services;

public class AddAdminUserIfNeeded : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AddAdminUserIfNeeded> _logger;

    public AddAdminUserIfNeeded(IServiceProvider serviceProvider, ILogger<AddAdminUserIfNeeded> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempt add admin");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var roleExists = await repository.RoleExists(Roles.Admin);
        if (!roleExists)
        {
            var addRoleResult = await repository.CreateRole(Roles.Admin);
            if (!addRoleResult.Success)
                throw new ApplicationException(addRoleResult.Error);
        }

        var hasAdmin = await repository.GetUsersCountWithRole(Roles.Admin) > 0;
        if (hasAdmin)
        {
            _logger.LogInformation("Admin already added");
            return;
        }

        var admin = await repository.FindByEmailAsync("admin");
        if (admin == null)
        {
            admin = await repository.CreateUser("admin", "admin", "Admin");
            _logger.LogInformation("Admin user created");
        }

        admin.Role = Roles.Admin;
        var saveUserResult = await repository.SaveUser(admin);
        if (!saveUserResult.Success)
            throw new ApplicationException(saveUserResult.Error);

        _logger.LogInformation("Admin user has been added");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop attempt add admin");
        return Task.CompletedTask;
    }
}