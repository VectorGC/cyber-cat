using System.Text.Json;
using TaskService.Repositories;
using TaskService.Repositories.InternalModels;

namespace TaskService.Services;

public class AutoLoadTasksToRepository : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<AutoLoadTasksToRepository> _logger;

    public AutoLoadTasksToRepository(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment, ILogger<AutoLoadTasksToRepository> logger)
    {
        _serviceProvider = serviceProvider;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start auto load tasks");

        await using var scope = _serviceProvider.CreateAsyncScope();
        var taskRepository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
        var testRepository = scope.ServiceProvider.GetRequiredService<ITestRepository>();

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "Tasks/auto_loading_tasks.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<List<TaskModel>>(stream, cancellationToken: cancellationToken);

        foreach (var task in tasks)
        {
            var alreadyContainsTask = await taskRepository.Contains(task.Id);
            if (alreadyContainsTask)
            {
                continue;
            }

            await taskRepository.Add(task.Id, task);
            await testRepository.Add(task.Id, task.Tests);
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