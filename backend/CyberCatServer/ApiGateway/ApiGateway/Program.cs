using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ApiGateway.Attributes;
using ApiGateway.Infrastructure;
using CommandLine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProtoBuf.Grpc.ClientFactory;
using Shared.Server.Configurations;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

var appArgs = Parser.Default.ParseArguments<ApiGatewayArgs>(args).Value;
var host = appArgs.UseHttps ? new Uri("https://0.0.0.0:443") : new Uri("http://0.0.0.0:80");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => { options.TokenValidationParameters = JwtTokenValidation.TokenValidationParameters; });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<BindUserAuthorizationFilter>();
    options.Filters.Add<BindPlayerAuthorizationFilter>();
});

// We create a user-friendly widget in Swagger for logging in with a username and password, rather than a JWT token.
builder.Services.AddSwaggerGen(options => { options.AddJwtSecurityDefinition(); });

builder.AddAuthServiceGrpcClient();
builder.AddTaskServiceGrpcClient();
builder.AddPlayerServiceGrpcClient();

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(host.Port, listenOptions =>
    {
        if (appArgs.UseHttps)
        {
            var certPem = File.ReadAllText(appArgs.CertificatePemPath);
            var keyPem = File.ReadAllText(appArgs.CertificateKeyPath);
            var x509Cert = X509Certificate2.CreateFromPem(certPem, keyPem);
            listenOptions.UseHttps(x509Cert);
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        _ =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            options.AddPolicy("signalr",
                builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true));
        });
});


var app = builder.Build();

// If we are in development mode (ASPNETCORE_ENVIRONMENT = Development).
if (app.Environment.IsDevelopment())
{
    // We showcase the API specification using Swagger.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        }
    );

    // Detailed errors in development mode.
    app.UseDeveloperExceptionPage();
}

app.UseHttpLogging();

if (appArgs.UseHttps)
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

// So that the Unity client can access this from a browser (ideally, specific domains need to be configured from which requests can be accepted).
// https://gitlab.com/karim.kimsanbaev/cyber-cat/-/issues/80
app.UseCors("CorsPolicy");
app.UseCors("signalr");

app.MapControllers();

app.Run();