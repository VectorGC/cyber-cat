using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITaskGrpcService
{
    Task<TaskDto> GetTask(StringProto taskId);
}