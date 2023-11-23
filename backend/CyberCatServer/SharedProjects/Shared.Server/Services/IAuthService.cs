using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Data;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IAuthService
{
    Task<Response<UserId>> CreateUser(CreateUserArgs args);
    Task<Response<AuthorizationToken>> GetAccessToken(GetAccessTokenArgs args);
    Task<Response> RemoveUser(RemoveUserArgs args);
    Task<Response<UserId>> FindByEmail(Args<string> email);
}

[ProtoContract(SkipConstructor = true)]
public record CreateUserArgs(
    [property: ProtoMember(1)] string Email,
    [property: ProtoMember(2)] string Password,
    [property: ProtoMember(3)] string UserName);

[ProtoContract(SkipConstructor = true)]
public record GetAccessTokenArgs(
    [property: ProtoMember(1)] string Email,
    [property: ProtoMember(2)] string Password);

[ProtoContract(SkipConstructor = true)]
public record RemoveUserArgs(
    [property: ProtoMember(1)] UserId Email,
    [property: ProtoMember(2)] string Password);