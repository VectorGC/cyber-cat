using System.Text.Json.Nodes;
using ApiGateway.Dto;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public interface ITaskService
{
    Task<JsonObject> GetTasksAsHierarchy();
    Task<TasksData> GetTasksAsFlat();
}

public class TaskService : ITaskService
{
    private readonly TasksHierarchyRepository _hierarchyRepository;
    private readonly TasksFlatRepository _flatRepository;

    public TaskService(IEnumerable<ITaskRepository> taskRepositories)
    {
        var repositories = (ITaskRepository[]) taskRepositories;
        _hierarchyRepository = repositories.OfType<TasksHierarchyRepository>().First();
        _flatRepository = repositories.OfType<TasksFlatRepository>().First();
    }

    public async Task<JsonObject> GetTasksAsHierarchy()
    {
        var obj = await _hierarchyRepository.GetTasks();
        return (JsonObject) obj;
    }

    public async Task<TasksData> GetTasksAsFlat()
    {
        var obj = await _flatRepository.GetTasks();
        return (TasksData) obj;
    }
}