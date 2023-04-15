using TaskService.Models;
using TaskService.Repositories;

namespace TaskService.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITaskRepository _taskCollection;
        public TasksService(ITaskRepository taskCollection)
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
