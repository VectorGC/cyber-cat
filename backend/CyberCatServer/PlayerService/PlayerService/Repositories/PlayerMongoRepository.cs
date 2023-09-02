using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbGenericRepository;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Models.Ids;
using Shared.Server.Exceptions.PlayerService;
using Shared.Server.Models;

namespace PlayerService.Repositories;

public class PlayerMongoRepository : BaseMongoRepository<long>, IPlayerRepository
{
    private readonly ILogger<PlayerMongoRepository> _logger;

    public PlayerMongoRepository(IOptions<PlayerServiceAppSettings> appSettings, ILogger<PlayerMongoRepository> logger)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
        _logger = logger;
    }

    public async Task<PlayerId> GetPlayerByUserId(UserId userId)
    {
        var player = await GetByIdAsync<PlayerDbModel>(userId.Value);
        if (player == null)
        {
            return null;
        }

        return new PlayerId(player.Id);
    }

    public async Task<PlayerId> CreatePlayer(UserId userId)
    {
        var playerModel = new PlayerDbModel()
        {
            Id = userId.Value
        };

        try
        {
            await AddOneAsync(playerModel);
        }
        catch (MongoWriteException writeException)
        {
            throw new IdentityPlayerException(writeException.Message);
        }

        _logger.LogInformation("Create player '{PlayerId}'", playerModel.Id);

        return new PlayerId(playerModel.Id);
    }

    public async Task RemovePlayer(PlayerId playerId)
    {
        await DeleteOneAsync<PlayerDbModel>(p => p.Id == playerId.Value);
    }

    public async Task SaveCode(PlayerId playerId, TaskId taskId, string solution)
    {
        var player = new PlayerDbModel
        {
            Id = playerId.Value
        };

        await UpdateOneAsync(player, player => player.Tasks[taskId.Value].Solution, solution);
    }

    public async Task<PlayerData> GetPlayerById(PlayerId playerId)
    {
        var player = await GetByIdAsync<PlayerDbModel>(playerId.Value);
        return player?.ToDto();
    }

    public async Task SetTaskStatus(PlayerId playerId, TaskId taskId, TaskProgressStatus status)
    {
        var playerModel = new PlayerDbModel()
        {
            Id = playerId.Value,
        };

        await UpdateOneAsync(playerModel, player => player.Tasks[taskId.Value].Status, status);
    }

    public async Task<TaskData> GetTaskData(PlayerId playerId, TaskId taskId)
    {
        var player = await GetByIdAsync<PlayerDbModel>(playerId.Value);
        var task = player.Tasks.GetValueOrDefault(taskId.Value) ?? new TaskProgressDbModel();

        return task.ToData();
    }

    public async Task AddBitcoins(PlayerId playerId, int bitcoins)
    {
        var player = await GetByIdAsync<PlayerDbModel>(playerId.Value);
        if (player == null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        player.BitcoinsAmount += bitcoins;
        await UpdateOneAsync(player);
    }

    public async Task RemoveBitcoins(PlayerId playerId, int bitcoins)
    {
        var player = await GetByIdAsync<PlayerDbModel>(playerId.Value);
        if (player == null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        if (player.BitcoinsAmount >= bitcoins)
        {
            player.BitcoinsAmount -= bitcoins;
            await UpdateOneAsync(player);
        }
        else
            throw new NotEnoughBitcoinsException(playerId, bitcoins);
    }
}