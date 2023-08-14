using ProtoBuf.Grpc.Server;
using TaskService;
using TaskService.Configurations;
using TaskService.GrpcServices;
using TaskService.Repositories;
using TaskService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TaskServiceAppSettings>(builder.Configuration);

builder.Services
    .AddScoped<ITaskRepository, TaskMongoRepository>()
    .AddHostedService<AutoLoadTasksToRepository>();

builder.Services.AddScoped<ITestRepository, TestMongoRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<TaskGrpcService>();
app.MapGrpcService<TestGrpcService>();

app.Run();