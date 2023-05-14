using AuthService;
using AuthService.Models;
using AuthService.Repositories;
using AuthService.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

#region | Authentication |

var appSettings = builder.Configuration.Get<AuthServiceAppSettings>();
builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>(appSettings.IdentityMongoDatabase.ConnectionString, appSettings.IdentityMongoDatabase.DatabaseName);

#endregion

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthUserRepository, AuthUserManagerRepository>();

builder.Services.AddCodeFirstGrpc(options =>
{
    options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
    options.EnableDetailedErrors = true;
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<AuthService.Services.AuthService>();

app.Run();

public partial class Program
{
}