using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;
using Shared.Server.Exceptions;

namespace PlayerService.Repositories;

public class PlayerMongoRepository : BaseMongoRepository<string>, IPlayerRepository
{
    public PlayerMongoRepository(IOptions<PlayerServiceAppSettings> appSettings)
        : base(appSettings.Value.MongoRepository.ConnectionString, appSettings.Value.MongoRepository.DatabaseName)
    {
    }

    public async Task CreatePlayer(string playerId)
    {
        var playerModel = await GetOneAsync<PlayerModel>(p => p.Id == playerId);
        if (playerModel != null)
            throw new PlayerAlreadyExistsException($"Error adding new player. Player with Id \"{playerId}\" already exists");

        playerModel = new PlayerModel()
        {
            Id = playerId
        };
        await AddOneAsync(playerModel);
    }

    public async Task RemovePlayer(string playerId)
    {
        await DeleteOneAsync<PlayerModel>(p => p.Id == playerId);
    }

    public async Task<PlayerDto> GetPlayerById(string playerId)
    {
        var player = await GetOneAsync<PlayerModel>(p => p.Id == playerId);
        if (player == null)
            throw PlayerNotFoundException.UserIdNotFound(playerId);
        return player.ToDto();
    }

    public async Task AddBitcoins(string playerId, int bitcoins)
    {
        var player = await GetOneAsync<PlayerModel>(p => p.Id == playerId);
        if (player == null)
            throw PlayerNotFoundException.UserIdNotFound(playerId);
        player.BitcoinsAmount += bitcoins;
        await UpdateOneAsync(player);
    }

    public async Task RemoveBitcoins(string playerId, int bitcoins)
    {
        var player = await GetOneAsync<PlayerModel>(p => p.Id == playerId);
        if (player == null)
            throw PlayerNotFoundException.UserIdNotFound(playerId);
        if (player.BitcoinsAmount >= bitcoins)
        {
            player.BitcoinsAmount -= bitcoins;
            await UpdateOneAsync(player);
        }
        else
            throw BitcoinOperationException.NotEnoughBitcoins(playerId, bitcoins);
    }
}