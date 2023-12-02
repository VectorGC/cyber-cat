using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace Shared.Server.Services;

[Service]
public interface IAuthService
{
    Task<UserId> CreateUser(CreateUserArgs args);
    Task<AuthorizationToken> GetAccessToken(GetAccessTokenArgs args);
    Task RemoveUser(RemoveUserArgs args);
    Task<UserId> FindByEmail(FindByEmailArgs args);
}

[ProtoContract(SkipConstructor = true)]
public record CreateUserArgs(
    [property: ProtoMember(1)] string Email,
    [property: ProtoMember(2)] string Password,
    [property: ProtoMember(3)] string UserName,
    [property: ProtoMember(4)] Roles Roles);

[ProtoContract(SkipConstructor = true)]
public record GetAccessTokenArgs(
    [property: ProtoMember(1)] string Email,
    [property: ProtoMember(2)] string Password);

[ProtoContract(SkipConstructor = true)]
public record RemoveUserArgs(
    [property: ProtoMember(1)] UserId Email,
    [property: ProtoMember(2)] string Password);

[ProtoContract(SkipConstructor = true)]
public record FindByEmailArgs(
    [property: ProtoMember(1)] string Email);