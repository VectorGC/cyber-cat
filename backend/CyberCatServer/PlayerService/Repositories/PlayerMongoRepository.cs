using System.Linq.Expressions;
using System.Runtime;
using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Server.Exceptions;

namespace PlayerService.Repositories;

public class PlayerMongoRepository : BaseMongoRepository, IPlayerRepository
{
    public PlayerMongoRepository(IOptions<PlayerServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
    }

    public async Task AddNewPlayer(long playerId)
    {
        var playerModel = await GetOneAsync<PlayerModel>(p => p.UserId == playerId);
        if (playerModel != null)
            throw new PlayerAlreadyExistsException($"Error adding new player. Player with Id \"{playerId}\" already exists");
        
        playerModel = new PlayerModel(playerId);
            await AddOneAsync(playerModel);
    }

    public async Task DeletePlayer(long playerId)
    {
        await DeleteOneAsync<PlayerModel>(p => p.UserId == playerId);
    }

    public async Task<PlayerDto> GetPlayerById(long playerId)
    {
        var player = await GetOneAsync<PlayerModel>(p => p.UserId == playerId);
        if (player == null)
            throw PlayerNotFoundException.UserIdNotFound(playerId);
        return player.ToDto();
    }

    public async Task AddBitcoinsToPlayer(long playerId, int bitcoins)
    {
        using (var session = await MongoDbContext.Client.StartSessionAsync())
        {
            session.StartTransaction();
            try
            {
                var player = await GetOneAsync<PlayerModel>(p => p.UserId == playerId);
                if (player == null)
                    throw PlayerNotFoundException.UserIdNotFound(playerId);
                player.BitcoinCount += bitcoins;
                await UpdateOneAsync(player);

                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw TransactionErrorException.AddTransactionError(playerId, bitcoins);
            }
            
        }
    }
}