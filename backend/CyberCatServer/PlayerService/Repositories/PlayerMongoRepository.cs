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
        var indexes = new List<Expression<Func<PlayerModel, object>>>
        {
            p => p.UserId,
            p => p.UserName,
            p => p.CompletedTasksCount
        };

        CreateCombinedTextIndexAsync(indexes);
        
    }

    public async Task AddNewPlayer(long userId)
    {
        var playerModel = await GetOneAsync<PlayerModel>(p => p.UserId == userId);
        if (playerModel != null)
            throw new PlayerAlreadyExistsException($"Error adding new player. Player with Id \"{userId}\" already exists");
        
        playerModel = new PlayerModel(userId);
            await AddOneAsync(playerModel);
    }

    public async Task DeletePlayer(long userId)
    {
        await DeleteOneAsync<PlayerModel>(p => p.UserId == userId);
    }

    public async Task<PlayerDto> GetPlayerById(long userId)
    {
        var player = await GetOneAsync<PlayerModel>(p => p.UserId == userId);
        if (player == null)
            throw PlayerNotFoundException.UserIdNotFound(userId);
        return player.ToDto();
    }
}