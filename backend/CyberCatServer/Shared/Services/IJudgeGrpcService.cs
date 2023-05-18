using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface IJudgeGrpcService
{
    Task<VerdictResponse> GetVerdict(SolutionArgs args);
}