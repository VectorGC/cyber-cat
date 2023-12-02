using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Server.Data;

namespace Shared.Server.Services;

[Service]
public interface ICodeLauncherGrpcService
{
    Task<OutputDto> Launch(LaunchCodeArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record LaunchCodeArgs(
    [property: ProtoMember(1)] string Solution,
    [property: ProtoMember(2)] string[] Inputs = null);