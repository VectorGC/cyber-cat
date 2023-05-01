using TaskService.Models;

namespace TaskService.Services
{
    public interface ITasksService
    {
        Task Add(ProgTaskDbModel task);
        Task<ProgTaskDbModel> GetTask(int id);
    }
}
