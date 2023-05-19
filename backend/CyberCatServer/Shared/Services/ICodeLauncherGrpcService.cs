using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface ICodeLauncherGrpcService
{
    Task<OutputDto> Launch(StringProto sourceCode);
}