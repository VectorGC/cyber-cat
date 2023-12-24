using JudgeService.Application;
using ProtoBuf.Grpc.Server;
using Shared.Server.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddCppLauncherGrpcClient();
builder.AddTaskServiceGrpcClient();

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<JudgeGrpcService>();

app.Run();