using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Models.TestCases;
using Shared.Server.Data;
using Shared.Server.ExternalData;

namespace Shared.Server.Services;

[Service]
public interface ITaskService
{
    Task<List<TaskDescription>> GetTasks();
    Task<TestCases> GetTestCases(TaskId taskId);
    Task OnTaskSolved(OnTaskSolvedArgs args);
    Task<List<SharedTaskProgressData>> GetSharedTasks();
    Task<WebHookResultStatus> ProcessWebHookTest();
}

[ProtoContract(SkipConstructor = true)]
public record OnTaskSolvedArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId);