namespace ApiGateway.Repositories;

public interface ITaskRepository
{
    Task<object> GetTasks();
}