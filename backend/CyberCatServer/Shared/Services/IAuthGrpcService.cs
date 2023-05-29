using ProtoBuf.Grpc.Configuration;
using Shared.Dto;
using Shared.Dto.Args;
using Shared.Dto.ProtoHelpers;

namespace Shared.Services;

[Service]
public interface IAuthGrpcService
{
    Task CreateUser(CreateUserArgs args);
    Task RemoveUser(StringProto email);
    Task<TokenDto> GetAccessToken(GetAccessTokenArgs args);
}