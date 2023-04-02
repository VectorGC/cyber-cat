using TaskServiceAPI.Models;

namespace TaskServiceAPI.Repositories
{
    public interface ITaskCollection
    {
        Task Add(ProgTask task);
        Task<ProgTask> GetTask(int id);
    }
}
