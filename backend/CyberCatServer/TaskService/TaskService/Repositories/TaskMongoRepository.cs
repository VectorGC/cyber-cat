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
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<TaskMongoRepository> _logger;

        public TaskMongoRepository(IOptions<TaskServiceAppSettings> appSettings, IHostEnvironment hostEnvironment, ILogger<TaskMongoRepository> logger)
            : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
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
            return await task.ToDescription(_hostEnvironment, _logger);
        }

        public async Task<bool> Contains(TaskId id)
        {
            return await AnyAsync<TaskDbModel>(task => task.Id == id.Value);
        }
    }
}