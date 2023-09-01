using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Dto.Args;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Server.Dto.Args;
using Shared.Server.Models;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IAuthGrpcService
{
    Task<Response<UserId>> CreateUser(CreateUserArgs args);
    Task<Response<string>> GetAccessToken(GetAccessTokenArgs args);
    Task Remove(UserId id);
    Task<Response<UserId>> FindByEmail(StringProto email);
}