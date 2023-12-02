using PlayerService.Application;
using PlayerService.Infrastructure;
using ProtoBuf.Grpc.Server;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddMongoDatabaseContext();
builder.Services.AddScoped<IPlayerRepository, PlayerMongoRepository>();

builder.AddJudgeServiceGrpcClient();
builder.AddTaskServiceGrpcClient();

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<PlayerGrpcService>();

app.Run();

namespace PlayerService
{
    internal class Program
    {
    }
}