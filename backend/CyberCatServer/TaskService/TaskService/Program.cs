using ProtoBuf.Grpc.Server;
using TaskService.GrpcServices;
using TaskService.Repositories;
using TaskService.Repositories.InternalModels;
using TaskService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ITestRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ISharedTaskProgressRepository, SharedTaskProgressMongoRepository>();
builder.Services.AddScoped<SharedTaskWebHookProcessor>();

builder.Services.AddHttpClient();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<TaskService.GrpcServices.TaskGrpcService>();

app.Run();