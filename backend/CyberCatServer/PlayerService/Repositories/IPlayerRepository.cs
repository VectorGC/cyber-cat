using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;

namespace PlayerService.Repositories;

public interface IPlayerRepository
{
    Task AddNewPlayer(long playerId);
    Task DeletePlayer(long playerId);
    Task<PlayerDto> GetPlayerById(long playerId);
    Task AddBitcoinsToPlayer(long playerId, int bitcoins);
}