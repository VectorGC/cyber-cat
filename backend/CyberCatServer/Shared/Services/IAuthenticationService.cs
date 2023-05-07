using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface IAuthenticationService
{
    Task<AccessTokenDto> GetAccessToken(GetAccessTokenArgsDto argsDto);
}