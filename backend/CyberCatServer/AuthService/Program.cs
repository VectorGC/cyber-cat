using AuthService;
using AuthService.Models;
using AuthService.Repositories;
using AuthService.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

#region | Authentication |

var appSettings = builder.Configuration.Get<AuthServiceAppSettings>();
builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>(appSettings.IdentityMongoDatabase.ConnectionString, appSettings.IdentityMongoDatabase.DatabaseName);

#endregion

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthUserRepository, AuthUserManagerRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<AuthService.Services.AuthGrpcService>();

app.Run();

public partial class Program
{
}