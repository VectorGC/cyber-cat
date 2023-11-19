using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Descriptions;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITaskService
{
    Task<Response<List<TaskId>>> GetTasks();
    Task<Response<TaskDescription>> GetTask(TaskId taskId);
    Task<Response<TestCases>> GetTestCases(TaskId taskId);
    Task OnTaskSolved(OnTaskSolvedArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record OnTaskSolvedArgs(
    [property: ProtoMember(1)] PlayerId PlayerId,
    [property: ProtoMember(2)] TaskId TaskId);