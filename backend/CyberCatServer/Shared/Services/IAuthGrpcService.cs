using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.Args;

namespace Shared.Services;

[Service]
public interface IAuthGrpcService
{
    Task<TokenResponse> GetAccessToken(GetAccessTokenArgs args);
}