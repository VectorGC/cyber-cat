using TaskService.Models;

namespace TaskService.Repositories
{
    public interface ITaskRepository
    {
        Task Add(ProgTaskDbModel task);
        Task<ProgTaskDbModel> GetTask(int id);
    }
}
