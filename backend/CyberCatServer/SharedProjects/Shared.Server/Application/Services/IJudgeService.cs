using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace Shared.Server.Application.Services;

[Service]
public interface IJudgeService
{
    Task<Verdict> GetVerdict(GetVerdictArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record GetVerdictArgs(
    [property: ProtoMember(1)] TaskId TaskId,
    [property: ProtoMember(2)] string Solution);

public static partial class ServiceExtensions
{
    public static IHttpClientBuilder AddJudgeServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("JudgeServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IJudgeService>(options => { options.Address = new Uri(connectionString); });
    }
}