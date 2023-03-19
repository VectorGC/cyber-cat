using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiGateway.Repositories;

public class TasksHierarchyRepository : ITaskRepository
{
    private readonly IHostEnvironment _hostEnvironment;

    public TasksHierarchyRepository(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<object> GetTasks()
    {
        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_hierarchy.json");

        await using var stream = File.OpenRead(fullPath);
        var tasks = await JsonSerializer.DeserializeAsync<JsonObject>(stream);

        if (tasks == null)
        {
            throw new NullReferenceException(nameof(tasks));
        }

        return tasks;
    }
}