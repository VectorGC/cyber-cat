using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Server.Domain;
using TaskService.Domain;

namespace TaskService.Infrastructure;

public class TaskEntityMapper
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger _logger;

    public TaskEntityMapper(IHostEnvironment hostEnvironment, ILogger<TaskEntityMapper> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public async Task<TaskDescription> ToDescription(TaskEntity task)
    {
        return new TaskDescription
        {
            Id = task.Id,
            Name = task.Name,
            Description = await GetDescription(task.Id),
            DefaultCode = await GetDefaultCode(task.Id)
        };
    }

    public List<TestCaseDescription> ToTestCaseDescriptions(TaskEntity task)
    {
        var testCases = new List<TestCaseDescription>();
        for (var i = 0; i < task.Tests.Count; i++)
        {
            var test = task.Tests[i].ToDescription(task.Id, i);
            testCases.Add(test);
        }

        return testCases;
    }

    public SharedTaskProgress ToSharedTaskProgressDomain(SharedTaskProgressEntity entity)
    {
        return new SharedTaskProgress()
        {
            TaskId = entity.Id,
            Status = entity.Status,
            UserId = entity.UserId
        };
    }

    private async Task<string> GetDescription(TaskId taskId)
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, $"Tasks/{taskId.Value}.md");

        if (!File.Exists(fullPath))
        {
            _logger.LogError("Not found description file by path \'{FullPath}\'", fullPath);
            return string.Empty;
        }

        using var stream = File.OpenText(fullPath);
        return await stream.ReadToEndAsync();
    }

    private async Task<string> GetDefaultCode(TaskId taskId)
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, $"Tasks/{taskId.Value}_default_code.cpp");

        if (!File.Exists(fullPath))
        {
            _logger.LogError("Not found default code file by path \'{FullPath}\'", fullPath);
            fullPath = Path.Combine(rootPath, "Tasks/default_code.cpp");
        }

        using var stream = File.OpenText(fullPath);
        return await stream.ReadToEndAsync();
    }
}