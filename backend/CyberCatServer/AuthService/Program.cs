using AuthService.Models;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProtoBuf.Grpc.Server;
using Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region | Authentication |

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { options.TokenValidationParameters = JwtTokenValidation.CreateTokenParameters(); });

var connectionString = builder.Configuration.GetConnectionString("MongoDatabase");
builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>(connectionString, "Identity");

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

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<AuthService.Services.AuthService>();

app.Run();

public partial class Program
{
}