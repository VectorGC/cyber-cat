using Shared.Models.Dto.Descriptions;
using Shared.Models.Models;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDescription> GetTask(TaskId taskId);
        Task Add(TaskId taskId, TaskDescription task);
        Task<bool> Contains(TaskId id);
    }
}