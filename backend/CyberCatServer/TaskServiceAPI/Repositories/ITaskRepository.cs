using TaskServiceAPI.Models;

namespace TaskServiceAPI.Repositories
{
    public interface ITaskRepository
    {
        Task Add(ProgTaskDbModel task);
        Task<ProgTaskDbModel> GetTask(int id);
    }
}
