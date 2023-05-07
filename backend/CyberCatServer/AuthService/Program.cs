using AuthService.Configurations;
using AuthService.Controllers;
using AuthService.Models;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProtoBuf.Grpc.Server;
using Shared;
using Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(option => { option.AddJwtSecurityDefinition(); });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { options.TokenValidationParameters = JwtTokenValidation.CreateTokenParameters(); });

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthUserRepository, AuthUserManagerRepository>();

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.AddIdentity<User, Role>()
    .AddMongoDbStores<User, Role, Guid>(mongoDbSettings.ConnectionString, mongoDbSettings.Name);

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<AuthenticationService>();

app.Run();

public partial class Program
{
}