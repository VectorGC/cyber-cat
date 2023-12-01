using MongoDB.Driver;
using MongoDbGenericRepository;
using PlayerService.Application;
using PlayerService.Domain;
using Shared.Models.Domain.Users;

namespace PlayerService.Infrastructure;

public class PlayerMongoRepository : BaseMongoRepository<long>, IPlayerRepository
{
    public PlayerMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
    {
    }

    public async Task<PlayerEntity> GetPlayer(UserId userId)
    {
        return await GetByIdAsync<PlayerEntity>(userId.Value);
    }

    public async Task Save(PlayerEntity player)
    {
        var result = await UpdateOneAsync(player);
        if (!result)
        {
            await AddOneAsync(player);
        }
    }
}