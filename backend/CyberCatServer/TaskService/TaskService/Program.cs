using ProtoBuf.Grpc.Server;
using TaskService.Repositories;
using TaskService.Repositories.InternalModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ITestRepository, TaskModelConstRepository>();
builder.Services.AddScoped<ISharedTaskProgressRepository, SharedTaskProgressMongoRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<TaskService.GrpcServices.TaskGrpcService>();

app.Run();