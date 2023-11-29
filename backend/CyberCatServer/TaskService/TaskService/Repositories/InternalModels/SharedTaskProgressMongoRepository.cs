using MongoDbGenericRepository;
using Shared.Models.Domain.Players;
using Shared.Models.Ids;
using Shared.Server.Data;

namespace TaskService.Repositories.InternalModels;

internal class SharedTaskProgressMongoRepository : BaseMongoRepository<string>, ISharedTaskProgressRepository
{
    private readonly ILogger<SharedTaskProgressMongoRepository> _logger;

    public SharedTaskProgressMongoRepository(IMongoDbContext dbContext, ILogger<SharedTaskProgressMongoRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<SharedTaskProgressData> GetTask(TaskId id)
    {
        return await GetOneAsync<SharedTaskProgressData>(task => task.Id == id.Value);
    }

    public async Task<SharedTaskProgressData> SetSolved(TaskId id, PlayerId playerId)
    {
        var model = new SharedTaskProgressData()
        {
            Id = id.Value,
            PlayerIdData = playerId.Value,
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