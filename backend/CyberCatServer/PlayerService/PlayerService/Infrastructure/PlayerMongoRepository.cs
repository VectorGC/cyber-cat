using MongoDbGenericRepository;
using PlayerService.Application;
using PlayerService.Domain;
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
            return new CreateResult(true, UserRepositoryError.None, player);
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
            var success = await UpdateOneAsync(player);
            if (!success)
            {
                return new UpdateResult(false, UserRepositoryError.Unknown);
            }

            return new UpdateResult(true, UserRepositoryError.None);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DeleteResult> Delete(UserId userId)
    {
        try
        {
            var countDeleted = await DeleteOneAsync<PlayerEntity>(player => player.Id == userId.Value);
            if (countDeleted == 0)
            {
                return new DeleteResult(false, UserRepositoryError.NotFound);
            }

            return new DeleteResult(true, UserRepositoryError.None);
        }
        catch (Exception)
        {
            throw;
        }
    }
}