using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Models.TestCases;
using Shared.Server.Data;
using Shared.Server.ExternalData;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITaskService
{
    Task<Response<List<TaskDescription>>> GetTasks();
    Task<Response<TestCases>> GetTestCases(TaskId taskId);
    Task OnTaskSolved(OnTaskSolvedArgs args);
    Task<Response<List<SharedTaskProgressData>>> GetSharedTasks();
    Task<Response<WebHookResultStatus>> ProcessWebHookTest();
}

[ProtoContract(SkipConstructor = true)]
public record OnTaskSolvedArgs(
    [property: ProtoMember(1)] UserId UserId,
    [property: ProtoMember(2)] TaskId TaskId);