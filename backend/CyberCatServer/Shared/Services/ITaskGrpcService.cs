using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.ProtoHelpers;

namespace Shared.Services;

[Service]
public interface ITaskGrpcService
{
    Task<TaskDto> GetTask(StringProto taskId);
}