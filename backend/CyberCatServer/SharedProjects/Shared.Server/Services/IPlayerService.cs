using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Data;
using Shared.Models.Domain.Players;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

/// <summary>
/// The Player service validates the player, sends solutions to the judge, and to support player persistence.
/// </summary>
[Service]
public interface IPlayerService
{
    Task<Response<PlayerId>> CreatePlayer(UserId userId);
    Task<Response> RemovePlayer(PlayerId playerId);
    Task<Response<PlayerData>> GetPlayerById(PlayerId playerId);
    Task<Response<PlayerId>> GetPlayerByUserId(UserId userId);
    Task<Response<Verdict>> GetVerdict(GetVerdictArgs args);
    Task<Response<TaskProgressData>> GetTaskData(GetTaskDataArgs args);
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