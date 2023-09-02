using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;
using TaskService.Configurations;
using TaskService.Repositories.InternalModels;

namespace TaskService.Repositories
{
    public class TaskMongoRepository : BaseMongoRepository<string>, ITaskRepository
    {
        public TaskMongoRepository(IOptions<TaskServiceAppSettings> appSettings)
            : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
        {
        }

        public async Task Add(TaskId id, TaskDescription task)
        {
            var taskModel = new TaskDbModel(id, task);
            await AddOneAsync(taskModel);
        }

        public async Task<List<TaskId>> GetTasks()
        {
            var tasks = await GetAllAsync<TaskDbModel>(_ => true);
            return tasks.Select(task => new TaskId(task.Id)).ToList();
        }

        public async Task<TaskDescription> GetTask(TaskId id)
        {
            var task = await GetByIdAsync<TaskDbModel>(id.Value);
            return task.ToDescription();
        }

        public async Task<bool> Contains(TaskId id)
        {
            return await AnyAsync<TaskDbModel>(task => task.Id == id.Value);
        }
    }
}