using JudgeService;
using JudgeService.GrpcServices;
using ProtoBuf.Grpc.ClientFactory;
using ProtoBuf.Grpc.Server;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JudgeServiceAppSettings>(builder.Configuration);

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var appSettings = builder.Configuration.Get<JudgeServiceAppSettings>();
builder.Services.AddCodeFirstGrpcClient<ICodeLauncherGrpcService>(options => { options.Address = appSettings.ConnectionStrings.CppLauncherServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<ITestGrpcService>(options => { options.Address = appSettings.ConnectionStrings.TestServiceGrpcAddress; });

var app = builder.Build();

app.MapGrpcService<JudgeGrpcService>();

app.Run();