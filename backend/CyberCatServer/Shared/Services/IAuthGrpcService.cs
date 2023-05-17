using ProtoBuf.Grpc.Configuration;
using Shared.Dto;

namespace Shared.Services;

[Service]
public interface IAuthGrpcService
{
    Task<TokenResponse> GetAccessToken(GetAccessTokenArgs args);
}