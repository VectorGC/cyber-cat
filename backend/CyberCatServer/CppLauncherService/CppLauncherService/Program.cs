using CppLauncherService.Application;
using CppLauncherService.Infrastructure;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CppLauncherAppSettings>(builder.Configuration);

switch (Environment.OSVersion.Platform)
{
    case PlatformID.Win32NT:
        builder.Services.AddScoped<IConsoleExecutor, WindowsConsoleExecutor>();
        break;
    default:
        builder.Services.AddScoped<IConsoleExecutor, LinuxConsoleExecutor>();
        break;
}

builder.Services.AddScoped<ICppCompileService, GppCompileService>();

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<CppLauncherGrpcService>();

app.Run();

namespace CppLauncherService
{
    internal class Program
    {
    }
}