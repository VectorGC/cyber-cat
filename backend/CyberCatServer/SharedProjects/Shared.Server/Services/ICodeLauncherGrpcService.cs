using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Args;
using Shared.Server.Dto;

namespace Shared.Server.Services;

[Service]
public interface ICodeLauncherGrpcService
{
    Task<OutputDto> Launch(LaunchCodeArgs args);
}