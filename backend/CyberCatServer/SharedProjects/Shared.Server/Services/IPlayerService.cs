using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IPlayerService
{
    Task<Response<List<TaskProgress>>> GetTasksProgress(GetTasksProgressArgs args);
    Task<Response<TaskProgress>> GetTaskProgress(GetTaskProgressArgs args);
    Task<Response<Verdict>> SubmitSolution(SubmitSolutionArgs args);
    Task<Response> RemovePlayer(RemovePlayerArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record GetTasksProgressArgs(
    [property: ProtoMember(1)] UserId UserId);

[ProtoContract(SkipConstructor = true)]
public record GetTaskProgressArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId);

[ProtoContract(SkipConstructor = true)]
public record SubmitSolutionArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId,
    [property: ProtoMember(3)] string Solution);

[ProtoContract(SkipConstructor = true)]
public record RemovePlayerArgs(
    [property: ProtoMember(1)] UserId UserId);