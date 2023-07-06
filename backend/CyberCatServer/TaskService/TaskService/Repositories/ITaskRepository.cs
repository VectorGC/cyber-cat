using Shared.Models;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDto> GetTask(string taskId);
        Task Add(string taskId, TaskDto task);
        Task<bool> Contains(string id);
    }
}