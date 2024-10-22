﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;

namespace Shared.Server.Application.Services;

[Service]
public interface IPlayerService
{
    Task<List<TaskProgress>> GetTasksProgress(UserId userId);
    Task<Verdict> SubmitSolution(SubmitSolutionArgs args);
    Task RemovePlayer(RemovePlayerArgs args);
    Task<List<UserId>> GetUsersWhoSolvedTask(TaskId taskId);
    Task SaveVerdictHistory(SaveVerdictHistoryArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record SubmitSolutionArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId,
    [property: ProtoMember(3)] string Solution);

[ProtoContract(SkipConstructor = true)]
public record RemovePlayerArgs(
    [property: ProtoMember(1)] UserId UserId);

[ProtoContract(SkipConstructor = true)]
public record SaveVerdictHistoryArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] List<Verdict> Verdicts);

public static partial class ServiceExtensions
{
    public static IHttpClientBuilder AddPlayerServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PlayerServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IPlayerService>(options => { options.Address = new Uri(connectionString); });
    }
}