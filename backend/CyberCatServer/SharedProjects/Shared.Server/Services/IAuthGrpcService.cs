using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto;
using Shared.Server.Dto.Args;
using Shared.Server.Models;

namespace Shared.Server.Services;

[Service]
public interface IAuthGrpcService
{
    Task<UserId> CreateUser(CreateUserArgs args);
    Task<StringProto> GetAccessToken(GetAccessTokenArgs args);
    Task Remove(UserId id);
    Task<FindUserByEmailResponse> FindByEmail(StringProto email);
}