using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface ITaskGrpcService
{
    Task<GetTaskResponse> GetTask(StringProto taskId);
}