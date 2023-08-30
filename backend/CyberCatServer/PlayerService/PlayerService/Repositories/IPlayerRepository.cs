using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Models.Models;
using Shared.Server.Models;

namespace PlayerService.Repositories;

public interface IPlayerRepository
{
    Task<PlayerId> CreatePlayer(UserId playerId);
    Task<PlayerId> GetPlayerByUserId(UserId userId);
    Task RemovePlayer(PlayerId playerId);
    Task<PlayerDto> GetPlayerById(PlayerId playerId);
    Task AddBitcoins(PlayerId playerId, int bitcoins);
    Task RemoveBitcoins(PlayerId playerId, int bitcoins);
    Task SetTaskStatus(PlayerId playerId, TaskId taskId, TaskProgressStatus status);
    Task<TaskData> GetTaskData(PlayerId playerId, TaskId taskId);
}