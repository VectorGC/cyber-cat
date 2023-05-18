using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface ISolutionGrpcService
{
    Task<GetSavedCodeResponse> GetSavedCode(GetSavedCodeArgs args);
    Task SaveCode(SolutionArgs args);
    Task RemoveCode(RemoveCodeArgs args);
}