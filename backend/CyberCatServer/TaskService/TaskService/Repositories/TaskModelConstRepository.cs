using Shared.Models.Descriptions;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;

namespace TaskService.Repositories;

internal class TaskModelConstRepository : ITaskRepository, ITestRepository
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<TaskModelConstRepository> _logger;

    public TaskModelConstRepository(IHostEnvironment hostEnvironment, ILogger<TaskModelConstRepository> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public Task<TaskDescription> GetTask(TaskId id)
    {
        return TaskDbModels.Tasks.FirstOrDefault(task => task.Id == id)?.ToDescription(_hostEnvironment, _logger);
    }

    public Task<List<TaskId>> GetTasks()
    {
        return Task.FromResult(TaskDbModels.Tasks.Select(task => new TaskId(task.Id)).ToList());
    }

    public Task<TestCases> GetTestCases(TaskId id)
    {
        var task = TaskDbModels.Tasks.FirstOrDefault(task => task.Id == id);
        return Task.FromResult(task?.Tests.ToDescription(id));
    }
}