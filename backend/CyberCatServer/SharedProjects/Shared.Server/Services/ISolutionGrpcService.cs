using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto.Args;
using Shared.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ISolutionGrpcService
{
    Task<StringProto> GetSavedCode(GetSavedCodeArgs args);
    Task SaveCode(SaveCodeArgs solution);
    Task RemoveCode(RemoveCodeArgs args);
}