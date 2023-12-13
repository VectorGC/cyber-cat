using MongoDbGenericRepository;
using PlayerService.Application;
using PlayerService.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using DeleteResult = PlayerService.Application.DeleteResult;
using UpdateResult = PlayerService.Application.UpdateResult;

namespace PlayerService.Infrastructure;

public class PlayerMongoRepository : BaseMongoRepository<long>, IPlayerRepository
{
    public PlayerMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public async Task<CreateResult> Create(UserId userId)
    {
        var player = new PlayerEntity(userId);
        try
        {
            await AddOneAsync(player);
            return new CreateResult(true, PlayerRepositoryError.None, player);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PlayerEntity> GetPlayer(UserId userId)
    {
        return await GetByIdAsync<PlayerEntity>(userId.Value);
    }

    public async Task<UpdateResult> Update(PlayerEntity player)
    {
        try
        {
            await UpdateOneAsync(player);
            return new UpdateResult(true, PlayerRepositoryError.None);
        }
        catch (Exception)
        {
            return new UpdateResult(false, PlayerRepositoryError.Unknown);
        }
    }

    public async Task<DeleteResult> Delete(UserId userId)
    {
        try
        {
            var countDeleted = await DeleteOneAsync<PlayerEntity>(player => player.Id == userId.Value);
            if (countDeleted == 0)
            {
                return new DeleteResult(false, PlayerRepositoryError.NotFound);
            }

            return new DeleteResult(true, PlayerRepositoryError.None);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async IAsyncEnumerable<PlayerEntity> GetPlayerWhoSolvedTask(TaskId taskId)
    {
        var whoSolvingTask = await GetAllAsync<PlayerEntity>(player => player.Tasks.ContainsKey(taskId));

        foreach (var player in whoSolvingTask)
        {
            var completeTask = player.Tasks[taskId].StatusType == TaskProgressStatusType.Complete;
            if (completeTask)
                yield return player;
        }
    }
}