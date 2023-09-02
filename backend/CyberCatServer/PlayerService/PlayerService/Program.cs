using PlayerService;
using PlayerService.GrpcServices;
using PlayerService.Repositories;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Server;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PlayerServiceAppSettings>(builder.Configuration);

builder.Services.AddScoped<IPlayerRepository, PlayerMongoRepository>();

var appSettings = builder.Configuration.Get<PlayerServiceAppSettings>();
builder.Services.AddCodeFirstGrpcClient<IJudgeGrpcService>(options => { options.Address = appSettings.ConnectionStrings.JudgeServiceGrpcAddress; });

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<PlayerGrpcService>();

app.Run();

// TODO: Add service to docker!

namespace PlayerService
{
    internal class Program
    {
    }
}