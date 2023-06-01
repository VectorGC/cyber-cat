using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITaskGrpcService
{
    Task<TaskDto> GetTask(StringProto taskId);
}