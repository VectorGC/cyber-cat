using Shared.Models.Ids;
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

        var tasks = new List<TaskDbModel>()
        {
            new()
            {
                Id = "tutorial",
                Name = "Hello cat!",
                Tests = new TestsDbModel()
                {
                    new()
                    {
                        Expected = "Hello cat!"
                    }
                }
            },
            new()
            {
                Id = "task-1",
                Name = "A + B",
                Tests = new TestsDbModel()
                {
                    new()
                    {
                        Inputs = new string[] {"1", "1"},
                        Expected = "2"
                    },
                    new()
                    {
                        Inputs = new string[] {"5", "10"},
                        Expected = "15"
                    },
                    new()
                    {
                        Inputs = new string[] {"-1000", "1000"},
                        Expected = "0"
                    }
                }
            }
        };

        foreach (var task in tasks)
        {
            var alreadyContainsTask = await taskRepository.Contains(new TaskId(task.Id));
            if (alreadyContainsTask)
            {
                continue;
            }

            await taskRepository.Add(new TaskId(task.Id), await task.ToDescription(_hostEnvironment, _logger));
            await testRepository.Add(task.Tests.ToDescription(task.Id));
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