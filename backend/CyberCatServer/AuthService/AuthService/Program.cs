using AuthService.Configurations;
using AuthService.GrpcServices;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using AuthService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.Get<AuthServiceAppSettings>();
builder.Services.AddIdentity<UserDbModel, Role>(AuthIdentity.SetServiceOptions).AddMongoDbStores<UserDbModel, Role, long>(appSettings.MongoRepository.ConnectionString, appSettings.MongoRepository.DatabaseName);

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services
    .AddScoped<IAuthUserRepository, AuthUserManagerRepository>()
    .AddHostedService<AddCyberCatUserToRepositoryOnStart>();

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