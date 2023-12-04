using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace Shared.Server.Application.Services;

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

public static class AuthServiceExtensions
{
    public static IHttpClientBuilder AddAuthServiceGrpcClient(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("AuthServiceGrpcEndpoint");
        return builder.Services.AddCodeFirstGrpcClient<IAuthService>(options => { options.Address = new Uri(connectionString); });
    }
}