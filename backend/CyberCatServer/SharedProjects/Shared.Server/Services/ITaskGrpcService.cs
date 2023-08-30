using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Models.Models;

namespace Shared.Server.Services;

[Service]
public interface ITaskGrpcService
{
    Task<TaskDescription> GetTask(TaskId taskId);
}