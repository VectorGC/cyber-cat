using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.Args;

namespace Shared.Server.Services;

[Service]
public interface ICodeLauncherGrpcService
{
    Task<OutputDto> Launch(LaunchCodeArgs args);
}