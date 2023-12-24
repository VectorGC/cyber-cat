using AuthService.Application;
using AuthService.Domain;
using AuthService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using Shared.Server.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+" +
        "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧЩЪЫЬЭЮЯ" +
        "#";
});

builder.Services
    .AddIdentity<UserEntity, RoleEntity>()
    .AddMongoDbStores<UserEntity, RoleEntity, string>(builder.Configuration.GetMongoDatabaseContext());

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services
    .AddScoped<IUserRepository, UserManagerRepository>()
    .AddScoped<UserEntityMapper>()
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