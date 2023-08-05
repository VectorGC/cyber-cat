using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;

namespace PlayerService.Repositories;

public interface IPlayerRepository
{
    Task AddNewPlayer(string playerId);
    Task DeletePlayer(string playerId);
    Task<PlayerDto> GetPlayerById(string playerId);
    Task AddBitcoinsToPlayer(string playerId, int bitcoins);
    Task TakeBitcoinsFromPlayer(string playerId, int bitcoins);
}