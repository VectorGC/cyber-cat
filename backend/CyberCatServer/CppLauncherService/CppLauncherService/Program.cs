using CppLauncherService;
using CppLauncherService.Services;
using CppLauncherService.Services.CppLaunchers;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CppLauncherAppSettings>(builder.Configuration);

builder.Services.AddScoped<IProcessExecutorProxy, ConsoleExecutorProxy>();
builder.Services.AddScoped<ICppExecutorOsSpecificService>(serviceProvider =>
{
    return Environment.OSVersion.Platform == PlatformID.Win32NT
        ? ActivatorUtilities.CreateInstance<WinExecutorService>(serviceProvider)
        : ActivatorUtilities.CreateInstance<LinuxExecutorService>(serviceProvider);
});
builder.Services.AddScoped<ICppFileCreator, TempCppFileCreator>();
builder.Services.AddScoped<ICppErrorFormatService, CppErrorFormatService>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<CppLauncherGrpcService>();

app.Run();

namespace CppLauncherService
{
    public partial class Program
    {
    }
}