using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Server.Services;

[Service]
public interface IJudgeGrpcService
{
    Task<VerdictDto> GetVerdict(SolutionDto solution);
}