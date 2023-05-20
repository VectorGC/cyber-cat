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
            var taskModel = task.To<TaskWithTestsModel>();
            taskModel.Id = id;

            await AddOneAsync(taskModel);
        }

        public async Task<ITask?> GetTask(string id)
        {
            var task = await GetOneAsync<TaskWithTestsModel>(task => task.Id == id);
            return task;
        }

        public async Task<bool> Contains(string id)
        {
            return await AnyAsync<TaskWithTestsModel>(task => task.Id == id);
        }
    }
}