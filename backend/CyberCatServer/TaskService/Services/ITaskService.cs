using TaskServiceAPI.Models;

namespace TaskServiceAPI.Services
{
    public interface ITaskService
    {
        Task Add(ProgTask task);
        Task<ProgTask> GetTask(int id);
    }
}
