using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Server.Infrastructure.Dto;

namespace Shared.Server.Application.Services;

[Service]
public interface ICodeLauncherService
{
    Task<OutputDto> Launch(LaunchCodeArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record LaunchCodeArgs(
    [property: ProtoMember(1)] string Solution,
    [property: ProtoMember(2)] string[] Inputs = null);

public static partial class ServiceExtensions
{
    public static IHttpClientBuilder AddCppLauncherGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CppLauncherServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<ICodeLauncherService>(options => { options.Address = new Uri(connectionString); });
    }
}