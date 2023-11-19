using MongoDbGenericRepository;
using Shared.Models.Ids;
using Shared.Server;
using Shared.Server.Data;
using Shared.Server.Ids;

namespace TaskService.Repositories.InternalModels;

internal class SharedTaskProgressMongoRepository : BaseMongoRepository<string>, ISharedTaskProgressRepository
{
    private readonly ILogger<SharedTaskProgressMongoRepository> _logger;

    public SharedTaskProgressMongoRepository(IConfiguration configuration, ILogger<SharedTaskProgressMongoRepository> logger)
        : base(configuration.GetMongoConnectionString(), configuration.GetMongoDatabase())
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
}