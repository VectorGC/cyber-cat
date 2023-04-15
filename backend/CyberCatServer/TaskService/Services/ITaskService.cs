using TaskServiceAPI.Models;

namespace TaskServiceAPI.Services
{
    public interface ITaskService
    {
        Task Add(ProgTaskDbModel task);
        Task<ProgTaskDbModel> GetTask(int id);
    }
}
