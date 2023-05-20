using System.Text.Json;
using TaskService.Repositories;
using TaskService.Repositories.InternalModels;

namespace TaskService.Services;

public class AutoLoadTasksRepository : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<AutoLoadTasksRepository> _logger;

    public AutoLoadTasksRepository(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment, ILogger<AutoLoadTasksRepository> logger)
    {
        _serviceProvider = serviceProvider;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start auto load tasks");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "Tasks/auto_loading_tasks.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<List<TaskWithTestsModel>>(stream, cancellationToken: cancellationToken);

        foreach (var task in tasks)
        {
            var alreadyContainsTask = await repository.Contains(task.Id);
            if (alreadyContainsTask)
            {
                continue;
            }

            await repository.Add(task.Id, task);
            _logger.LogInformation("Not found task '{Id}', it's has been added", task.Id);
        }

        _logger.LogInformation("Complete auto load tasks");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop auto load tasks");
        return Task.CompletedTask;
    }
}