using Shared.Models;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<ITask?> GetTask(string taskId);
        Task Add(string taskId, ITask task);
        Task<bool> Contains(string id);
    }
}