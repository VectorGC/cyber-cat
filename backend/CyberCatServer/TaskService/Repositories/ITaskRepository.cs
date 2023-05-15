using Shared.Models;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<ITask> GetTask(string taskId);
    }
}