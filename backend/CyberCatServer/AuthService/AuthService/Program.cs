using AuthService.Configurations;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using AuthService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using Shared.Server.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddIdentity<UserDbModel, RoleDbModel>(AuthIdentity.IdentityOptions)
    .AddMongoDbStores<UserDbModel, RoleDbModel, string>(builder.Configuration.GetDatabaseConnectionString(), builder.Configuration.GetDatabaseName());

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services
    .AddScoped<IUserRepository, UserManagerRepository>()
    .AddHostedService<AddAdminUserIfNeeded>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<AuthService.GrpcServices.AuthService>();

app.Run();

namespace AuthService
{
    internal class Program
    {
    }
}