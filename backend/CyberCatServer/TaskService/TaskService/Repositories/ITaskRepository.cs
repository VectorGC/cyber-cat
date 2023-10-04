using Shared.Models.Descriptions;
using Shared.Models.Ids;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDescription> GetTask(TaskId taskId);
        Task Add(TaskId taskId, TaskDescription task);
        Task<List<TaskId>> GetTasks();
        Task<bool> Contains(TaskId id);
    }
}