using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto.Args;
using Shared.Server.Models;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

/// <summary>
/// The Player service validates the player, sends solutions to the judge, and to support player persistence.
/// </summary>
[Service]
public interface IPlayerGrpcService
{
    Task<Response<PlayerId>> CreatePlayer(UserId userId);
    Task RemovePlayer(PlayerId playerId);
    Task<Response<PlayerDto>> GetPlayerById(PlayerId playerId);
    Task<Response<PlayerId>> GetPlayerByUserId(UserId userId);
    Task<VerdictDto> GetVerdict(GetVerdictForPlayerArgs args);
    Task<TaskData> GetTaskData(GetTaskDataArgs args);
    Task AddBitcoinsToPlayer(GetPlayerBtcArgs playerBtcArgs);
    Task TakeBitcoinsFromPlayer(GetPlayerBtcArgs playerBtcArgs);
}