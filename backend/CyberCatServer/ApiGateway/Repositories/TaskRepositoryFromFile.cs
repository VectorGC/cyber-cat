using System.Text.Json;
using System.Text.Json.Nodes;
using ApiGateway.Dto;
using ApiGateway.Models;

namespace ApiGateway.Repositories;

public class TaskRepositoryFromFile : ITaskRepository
{
    private readonly IHostEnvironment _hostEnvironment;

    public TaskRepositoryFromFile(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<object> GetTasks()
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<JsonObject>(stream);

        if (tasks == null)
        {
            throw new NullReferenceException(nameof(tasks));
        }

        return tasks;
    }

    public async Task<ITask> GetTask(string taskId)
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<Dictionary<string, TaskDto>>(stream);

        return tasks[taskId];
    }
}