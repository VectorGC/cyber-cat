using System.Text.Json;
using ApiGateway.Dto;
using ApiGateway.Models;

namespace ApiGateway.Repositories;

public class TasksFlatRepository : ITaskRepository
{
    private readonly IHostEnvironment _hostEnvironment;

    public TasksFlatRepository(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<object> GetTasks()
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_flat.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<TasksData>(stream);

        if (tasks == null)
        {
            throw new NullReferenceException(nameof(tasks));
        }

        return tasks;
    }

    public Task<ITask> GetTask(string taskId)
    {
        throw new NotImplementedException();
    }
}