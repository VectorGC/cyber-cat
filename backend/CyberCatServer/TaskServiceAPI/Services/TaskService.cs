using TaskServiceAPI.Models;
using TaskServiceAPI.Repositories;

namespace TaskServiceAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskCollection;
        public TaskService(ITaskRepository taskCollection)
        {
            _taskCollection = taskCollection;
        }

        public async Task Add(ProgTaskDbModel task)
        {
            await _taskCollection.Add(task);
        }

        public async Task<ProgTaskDbModel> GetTask(int id)
        {
           return await _taskCollection.GetTask(id);
        }
    }
}
