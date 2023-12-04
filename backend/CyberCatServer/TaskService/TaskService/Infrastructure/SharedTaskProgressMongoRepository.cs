using MongoDbGenericRepository;
using Shared.Models.Domain.Tasks;
using TaskService.Application;
using TaskService.Domain;

namespace TaskService.Infrastructure;

internal class SharedTaskProgressMongoRepository : BaseMongoRepository<string>, ISharedTaskProgressRepository
{
    public SharedTaskProgressMongoRepository(IMongoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SharedTaskProgressEntity> GetTaskProgress(TaskId id)
    {
        return await GetOneAsync<SharedTaskProgressEntity>(task => task.Id == id.Value);
    }

    public async Task<List<SharedTaskProgressEntity>> GetTasksProgresses()
    {
        var tasks = await GetAllAsync<SharedTaskProgressEntity>(task => true);
        return tasks;
    }

    public async Task Update(SharedTaskProgressEntity progress)
    {
        if (await UpdateOneAsync(progress))
            await AddOneAsync(progress);
    }
}