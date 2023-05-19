using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.Args;

namespace Shared.Services;

[Service]
public interface ISolutionGrpcService
{
    Task<StringProto> GetSavedCode(GetSavedCodeArgs args);
    Task SaveCode(SolutionDto solution);
    Task RemoveCode(RemoveCodeArgs args);
}