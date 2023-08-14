using Shared.Models.Dto;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDto> GetTask(string taskId);
        Task Add(string taskId, TaskDto task);
        Task<bool> Contains(string id);
    }
}