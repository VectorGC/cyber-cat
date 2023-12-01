using TaskService.Domain;

namespace TaskService.Application
{
    public interface ITaskRepository
    {
        Task<List<TaskEntity>> GetTasks();
    }
}