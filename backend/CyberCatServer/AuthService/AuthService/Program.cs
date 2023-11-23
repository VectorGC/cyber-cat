using AuthService.Application;
using AuthService.Domain;
using AuthService.Domain.Models;
using AuthService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using Shared.Server.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 3;
});

builder.Services
    .AddIdentity<UserModel, RoleModel>()
    .AddMongoDbStores<UserModel, RoleModel, string>(builder.Configuration.GetDatabaseConnectionString(), builder.Configuration.GetDatabaseName());

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services
    .AddScoped<IUserRepository, UserManagerRepository>()
    .AddHostedService<AddAdminUserIfNeeded>();

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<AuthGrpcService>();

app.Run();

namespace AuthService
{
    internal class Program
    {
    }
}