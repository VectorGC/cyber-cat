using CompilerServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<ICompileService>(serviceProvider =>
{
    return Environment.OSVersion.Platform == PlatformID.Win32NT
        ? ActivatorUtilities.CreateInstance<WinCompileService>(serviceProvider)
        : ActivatorUtilities.CreateInstance<LinuxCompileService>(serviceProvider);
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || true)
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();