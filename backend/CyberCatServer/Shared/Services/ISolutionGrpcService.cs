using ProtoBuf.Grpc.Configuration;
using Shared.Dto.Args;
using Shared.Dto.ProtoHelpers;

namespace Shared.Services;

[Service]
public interface ISolutionGrpcService
{
    Task<StringProto> GetSavedCode(GetSavedCodeArgs args);
    Task SaveCode(SaveCodeArgs solution);
    Task RemoveCode(RemoveCodeArgs args);
}