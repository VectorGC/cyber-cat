using CompilerServiceAPI;
using CompilerServiceAPI.Services;
using CompilerServiceAPI.Services.CppLaunchers;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CppLauncherAppSettings>(builder.Configuration);

builder.Services.AddScoped<IProcessExecutorProxy, ConsoleExecutorProxy>();
builder.Services.AddScoped<ICppLauncherService>(serviceProvider =>
{
    return Environment.OSVersion.Platform == PlatformID.Win32NT
        ? ActivatorUtilities.CreateInstance<WinCompileService>(serviceProvider)
        : ActivatorUtilities.CreateInstance<LinuxCompileService>(serviceProvider);
});
builder.Services.AddScoped<ICppFileCreator, TempCppFileCreator>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<CppLauncherGrpcService>();

app.Run();

namespace CompilerServiceAPI
{
    public partial class Program
    {
    }
}