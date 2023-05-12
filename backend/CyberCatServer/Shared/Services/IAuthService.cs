using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface IAuthService
{
    Task<AccessTokenDto> GetAccessToken(GetAccessTokenArgsDto argsDto);
}