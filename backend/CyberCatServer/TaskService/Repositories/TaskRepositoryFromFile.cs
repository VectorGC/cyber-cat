using System.Text.Json;
using Shared.Dto;
using Shared.Models;

namespace TaskService.Repositories;

public class TaskRepositoryFromFile : ITaskRepository
{
    private readonly IHostEnvironment _hostEnvironment;

    public TaskRepositoryFromFile(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<ITask> GetTask(string taskId)
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "tasks.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<Dictionary<string, TaskDto>>(stream);

        return tasks[taskId];
    }
}