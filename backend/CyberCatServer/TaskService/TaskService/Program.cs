using ProtoBuf.Grpc.Server;
using TaskService.GrpcServices;
using TaskService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ITestRepository, TaskModelConstRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<TaskGrpcService>();

app.Run();