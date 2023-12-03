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
using Shared.Models.Domain.Users;
using Shared.Server.Data;
using Shared.Server.ExternalData;

namespace Shared.Server.Services;

[Service]
public interface ITaskService
{
    Task<List<TaskDescription>> GetTasks();
    Task<List<TestCaseDescription>> GetTestCases(TaskId taskId);
    Task OnTaskSolved(OnTaskSolvedArgs args);
    Task<List<SharedTaskProgressData>> GetSharedTasks();
    Task<WebHookResultStatus> ProcessWebHookTest();
}

[ProtoContract(SkipConstructor = true)]
public record OnTaskSolvedArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId);

public static class TaskServiceExtensions
{
    public static IHttpClientBuilder AddTaskServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("TaskServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<ITaskService>(options => { options.Address = new Uri(connectionString); });
    }
}