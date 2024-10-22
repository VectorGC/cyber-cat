using ProtoBuf.Grpc.Server;
using Shared.Server.Application.Services;
using TaskService.Application;
using TaskService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddMongoDatabaseContext();

builder.Services.AddScoped<ITaskRepository, TaskModelConstCaseRepository>();
builder.Services.AddScoped<TaskEntityMapper>();

builder.Services.AddHttpClient();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<TaskGrpcService>();

app.Run();