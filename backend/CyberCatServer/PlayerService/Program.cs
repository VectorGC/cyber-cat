using PlayerService;
using PlayerService.GrpcServices;
using PlayerService.Repositories;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PlayerServiceAppSettings>(builder.Configuration);

builder.Services.AddScoped<IPlayerRepository, PlayerMongoRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<PlayerGrpcService>();

app.Run();

namespace PlayerService
{
    internal class Program
    {
    }
}