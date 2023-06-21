using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;

namespace Shared.Server.Services;

[Service]
public interface IAuthGrpcService
{
    Task<TokenDto> GetAccessToken(GetAccessTokenArgs args);
}