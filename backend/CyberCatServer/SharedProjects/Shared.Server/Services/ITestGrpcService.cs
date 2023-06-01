using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ITestGrpcService
{
    Task<TestsDto> GetTests(StringProto taskId);
}