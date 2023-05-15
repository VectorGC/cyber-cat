using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models;

namespace TaskService.Repositories
{
    public class TaskMongoRepository : BaseMongoRepository<string>, ITaskRepository
    {
        public TaskMongoRepository(IOptions<TaskServiceAppSettings> appSettings)
            : base(appSettings.Value.MongoTaskRepository.ConnectionString, appSettings.Value.MongoTaskRepository.DatabaseName)
        {
        }

        public async Task Add(string id, ITask task)
        {
            var taskModel = TaskModel.FromTask(id, task);
            await AddOneAsync(taskModel);
        }

        public async Task<ITask> GetTask(string id)
        {
            var task = await GetOneAsync<TaskModel>(task => task.Id == id);
            return task;
        }
    }
}