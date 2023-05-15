using ApiGateway.Models;

namespace ApiGateway.Repositories;

public interface ITaskRepository
{
    Task<ITask> GetTask(string taskId);
}