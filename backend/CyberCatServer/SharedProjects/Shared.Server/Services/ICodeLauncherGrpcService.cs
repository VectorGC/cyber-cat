using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Server.Data;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface ICodeLauncherGrpcService
{
    Task<Response<OutputDto>> Launch(LaunchCodeArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record LaunchCodeArgs(
    [property: ProtoMember(1)] string Solution,
    [property: ProtoMember(2)] string[] Inputs = null);