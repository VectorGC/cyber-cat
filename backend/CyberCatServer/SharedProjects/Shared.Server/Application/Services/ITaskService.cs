using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace Shared.Server.Application.Services;

[Service]
public interface ITaskService
{
    Task<List<TaskDescription>> GetTasks();
    Task<List<TestCaseDescription>> GetTestCases(TaskId taskId);
    Task<NeedSendWebHookResponse> NeedSendWebHook(TaskId taskId);
}

[ProtoContract(SkipConstructor = true)]
public record NeedSendWebHookResponse([property: ProtoMember(1)] bool NeedSendWebHook);

public static partial class ServiceExtensions
{
    public static IHttpClientBuilder AddTaskServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("TaskServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<ITaskService>(options => { options.Address = new Uri(connectionString); });
    }
}