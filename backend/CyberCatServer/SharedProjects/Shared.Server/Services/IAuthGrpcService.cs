using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.Args;
using Shared.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IAuthGrpcService
{
    Task<TokenDto> GetAccessToken(GetAccessTokenArgs args);
}