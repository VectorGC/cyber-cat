using Shared.Models.Dto;

namespace PlayerService.Repositories;

public interface IPlayerRepository
{
    Task CreatePlayer(string playerId);
    Task RemovePlayer(string playerId);
    Task<PlayerDto> GetPlayerById(string playerId);
    Task AddBitcoins(string playerId, int bitcoins);
    Task RemoveBitcoins(string playerId, int bitcoins);
}