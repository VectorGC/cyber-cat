using CppLauncherService;
using CppLauncherService.GrpcServices;
using CppLauncherService.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CppLauncherAppSettings>(builder.Configuration);

builder.Services.AddScoped<IProcessExecutorProxy, ConsoleExecutorProxy>();
builder.Services.AddScoped<ICppFileCreator, TempCppFileCreator>();
builder.Services.AddScoped<ICppErrorFormatService, CppErrorFormatService>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<CppLauncherGrpcService>();

app.Run();

namespace CppLauncherService
{
    internal class Program
    {
    }
}