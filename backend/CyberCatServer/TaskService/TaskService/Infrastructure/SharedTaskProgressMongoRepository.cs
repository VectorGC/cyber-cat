using MongoDbGenericRepository;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Server.Data;
using TaskService.Application;

namespace TaskService.Infrastructure;

internal class SharedTaskProgressMongoRepository : BaseMongoRepository<string>, ISharedTaskProgressRepository
{
    public SharedTaskProgressMongoRepository(IMongoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SharedTaskProgressData> GetTask(TaskId id)
    {
        return await GetOneAsync<SharedTaskProgressData>(task => task.Id == id.Value);
    }

    public async Task<SharedTaskProgressData> SetSolved(TaskId id, UserId userId)
    {
        var model = new SharedTaskProgressData()
        {
            Id = id.Value,
            UserId = userId.Value,
            Status = SharedTaskStatus.Solved
        };

        var task = await GetTask(id);
        if (task == null)
        {
            await AddOneAsync(model);
            return model;
        }

        if (task.Status == SharedTaskStatus.NotSolved)
        {
            await UpdateOneAsync(model);
            return model;
        }

        return null;
    }

    public async Task<List<SharedTaskProgressData>> GetTasks()
    {
        var tasks = await GetAllAsync<SharedTaskProgressData>(task => true);
        return tasks;
    }
}