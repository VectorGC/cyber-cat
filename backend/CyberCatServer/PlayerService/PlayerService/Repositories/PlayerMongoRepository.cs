using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Models.Models;
using Shared.Server.Exceptions;
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
        var player = await GetOneAsync<PlayerDbModel>(p => p.Id == userId.Value);
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

        await AddOneAsync(playerModel);
        _logger.LogInformation("Create player '{PlayerId}'", playerModel.Id);

        return new PlayerId(playerModel.Id);
    }

    public async Task RemovePlayer(PlayerId playerId)
    {
        await DeleteOneAsync<PlayerDbModel>(p => p.Id == playerId.Value);
    }

    public async Task<PlayerDto> GetPlayerById(PlayerId playerId)
    {
        var player = await GetOneAsync<PlayerDbModel>(p => p.Id == playerId.Value);
        return player?.ToDto();
    }

    public async Task SetTaskStatus(PlayerId playerId, TaskId taskId, TaskProgressStatus status)
    {
        var playerModel = new PlayerDbModel()
        {
            Id = playerId.Value,
            Tasks = new Dictionary<string, TaskProgressDbModel>()
            {
                [taskId.Value] = new TaskProgressDbModel()
                {
                    Status = status
                }
            }
        };

        await UpdateOneAsync(playerModel);
    }

    public async Task<TaskData> GetTaskData(PlayerId playerId, TaskId taskId)
    {
        var player = await GetOneAsync<PlayerDbModel>(player => player.Id == playerId.Value);
        var task = player.Tasks.GetValueOrDefault(taskId.Value) ?? new TaskProgressDbModel();

        return task.ToData();
    }

    public async Task AddBitcoins(PlayerId playerId, int bitcoins)
    {
        var player = await GetOneAsync<PlayerDbModel>(p => p.Id == playerId.Value);
        if (player == null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        player.BitcoinsAmount += bitcoins;
        await UpdateOneAsync(player);
    }

    public async Task RemoveBitcoins(PlayerId playerId, int bitcoins)
    {
        var player = await GetOneAsync<PlayerDbModel>(p => p.Id == playerId.Value);
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