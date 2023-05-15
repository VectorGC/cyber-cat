using ApiGateway.Models;

namespace ApiGateway.Repositories;

public interface ITaskRepository
{
    Task<object> GetTasks();
    Task<ITask> GetTask(string taskId);
}