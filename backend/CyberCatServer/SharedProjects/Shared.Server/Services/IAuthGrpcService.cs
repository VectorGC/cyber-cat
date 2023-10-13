using System.Threading.Tasks;
using ProtoBuf;
using ProtoBuf.Grpc.Configuration;
using Shared.Server.Data;
using Shared.Server.Ids;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IAuthGrpcService
{
    Task<Response<UserId>> CreateUser(CreateUserArgs args);
    Task<Response<string>> GetAccessToken(GetAccessTokenArgs args);
    Task<Response> Remove(RemoveArgs args);
    Task<Response> RemoveDev(RemoveDevArgs args);
    Task<Response<UserId>> FindByEmail(Args<string> email);
    Task<Response<UserDto>> SignInWithVk(OAuthSignIn args);
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
public record RemoveArgs(
    [property: ProtoMember(1)] UserId Email,
    [property: ProtoMember(2)] string Password);

[ProtoContract(SkipConstructor = true)]
public record RemoveDevArgs(
    [property: ProtoMember(1)] string Email);

[ProtoContract(SkipConstructor = true)]
public record OAuthSignIn(
    [property: ProtoMember(1)] string Email,
    [property: ProtoMember(2)] string UserName);