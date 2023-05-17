using AuthService;
using AuthService.Repositories;
using AuthService.Repositories.InternalModels;
using AuthService.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Authentication.
var appSettings = builder.Configuration.Get<AuthServiceAppSettings>();
builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>(appSettings.MongoRepository.ConnectionString, appSettings.MongoRepository.DatabaseName);

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthUserRepository, AuthUserManagerRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<AuthGrpcService>();

app.Run();

namespace AuthService
{
    public partial class Program
    {
    }
}