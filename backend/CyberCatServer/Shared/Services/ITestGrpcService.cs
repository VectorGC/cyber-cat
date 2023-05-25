using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface ITestGrpcService
{
    Task<ListProto<TestDto>> GetTests(StringProto taskId);
}