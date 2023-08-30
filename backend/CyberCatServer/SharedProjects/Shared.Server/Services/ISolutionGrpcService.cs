using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto.Args;

namespace Shared.Server.Services;

[Service]
public interface ISolutionGrpcService
{
    Task<StringProto> GetSavedCode(GetSavedCodeArgs args);
    Task SaveCode(SaveCodeArgs solution);
    Task RemoveCode(RemoveCodeArgs args);
}