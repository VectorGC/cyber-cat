using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Data;
using Shared.Models.Ids;
using Shared.Models.Models;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

/// <summary>
/// The Player service validates the player, sends solutions to the judge, and to support player persistence.
/// </summary>
[Service]
public interface IPlayerGrpcService
{
    Task<Response<PlayerId>> CreatePlayer(UserId userId);
    Task<Response> RemovePlayer(PlayerId playerId);
    Task<Response<PlayerData>> GetPlayerById(PlayerId playerId);
    Task<Response<PlayerId>> GetPlayerByUserId(UserId userId);
    Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args);
    Task<Response<VerdictV2>> GetVerdictV2(GetVerdictArgs args);
    Task<Response<TaskData>> GetTaskData(GetTaskDataArgs args);
    Task<Response> AddBitcoinsToPlayer(GetPlayerBtcArgs playerBtcArgs);
    Task<Response> TakeBitcoinsFromPlayer(GetPlayerBtcArgs playerBtcArgs);
}

[ProtoContract(SkipConstructor = true)]
public record GetVerdictArgs(
    [property: ProtoMember(1)] PlayerId PlayerId,
    [property: ProtoMember(2)] TaskId TaskId,
    [property: ProtoMember(3)] string Solution);

[ProtoContract(SkipConstructor = true)]
public record GetTaskDataArgs(
    [property: ProtoMember(1)] PlayerId PlayerId,
    [property: ProtoMember(2)] TaskId TaskId);

[ProtoContract(SkipConstructor = true)]
public record GetPlayerBtcArgs(
    [property: ProtoMember(1)] PlayerId PlayerId,
    [property: ProtoMember(2)] int BitcoinsAmount);