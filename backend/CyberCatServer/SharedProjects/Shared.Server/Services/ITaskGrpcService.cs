using System.Collections.Generic;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITaskGrpcService
{
    Task<Response<List<TaskId>>> GetTasks();
    Task<Response<TaskDescription>> GetTask(TaskId taskId);
}