using ProtoBuf.Grpc.Server;
using Shared.Server.Services;
using TaskService.Application;
using TaskService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddMongoDatabaseContext();

builder.Services.AddScoped<ITaskRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ITestRepository, TaskModelConstRepository>();
builder.Services.AddScoped<TaskEntityMapper>();
builder.Services.AddScoped<ISharedTaskProgressRepository, SharedTaskProgressMongoRepository>();
builder.Services.AddScoped<SharedTaskWebHookProcessor>();

builder.Services.AddHttpClient();
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<TaskGrpcService>();

app.Run();