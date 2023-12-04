using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Verdicts;

namespace Shared.Server.Application.Services;

[Service]
public interface IJudgeService
{
    Task<Verdict> GetVerdict(SubmitSolutionArgs args);
}

public static class JudgeServiceExtensions
{
    public static IHttpClientBuilder AddJudgeServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("JudgeServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IJudgeService>(options => { options.Address = new Uri(connectionString); });
    }
}