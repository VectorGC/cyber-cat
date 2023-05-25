using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface ITestGrpcService
{
    Task<TestsDto> GetTests(StringProto taskId);
}