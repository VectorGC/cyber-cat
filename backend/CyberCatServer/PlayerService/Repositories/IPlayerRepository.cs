using PlayerService.Repositories.InternalModels;
using Shared.Models.Dto;

namespace PlayerService.Repositories;

public interface IPlayerRepository
{
    Task AddNewPlayer(long userId);
    Task DeletePlayer(long userId);
    Task<PlayerDto> GetPlayerById(long userId);
}