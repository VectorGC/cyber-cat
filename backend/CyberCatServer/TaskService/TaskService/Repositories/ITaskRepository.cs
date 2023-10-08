using Shared.Models.Descriptions;
using Shared.Models.Ids;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDescription> GetTask(TaskId id);
        Task<List<TaskId>> GetTasks();
    }
}