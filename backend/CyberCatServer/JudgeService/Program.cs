using JudgeService;
using JudgeService.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JudgeServiceAppSettings>(builder.Configuration);

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<JudgeGrpcService>();

app.Run();