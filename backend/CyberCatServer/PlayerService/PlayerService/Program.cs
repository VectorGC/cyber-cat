using PlayerService.GrpcServices;
using PlayerService.Repositories;
using ProtoBuf.Grpc.Server;
using Shared.Server.Configurations;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongoDatabaseContext();
builder.Services.AddScoped<IPlayerRepository, PlayerMongoRepository>();

builder.AddJudgeServiceGrpcClient();
builder.AddTaskServiceGrpcClient();

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