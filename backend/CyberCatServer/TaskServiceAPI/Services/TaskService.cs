using TaskServiceAPI.Models;
using TaskServiceAPI.Repositories;

namespace TaskServiceAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskCollection _taskCollection;
        public TaskService(ITaskCollection taskCollection)
        {
            _taskCollection = taskCollection;
        }

        public async Task Add(ProgTask task)
        {
            await _taskCollection.Add(task);
        }

        public async Task<ProgTask> GetTask(int id)
        {
           return await _taskCollection.GetTask(id);
        }
    }
}
