using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models;
using TaskService.Repositories.InternalModels;

namespace TaskService.Repositories
{
    public class TaskMongoRepository : BaseMongoRepository<string>, ITaskRepository
    {
        public TaskMongoRepository(IOptions<TaskServiceAppSettings> appSettings)
            : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
        {
        }

        public async Task Add(string id, ITask task)
        {
            var taskModel = new TaskModel(id, task);
            await AddOneAsync(taskModel);
        }

        public async Task<ITask?> GetTask(string id)
        {
            var task = await GetOneAsync<TaskModel>(task => task.Id == id);
            return task;
        }

        public async Task<bool> Contains(string id)
        {
            return await AnyAsync<TaskModel>(task => task.Id == id);
        }
    }
}