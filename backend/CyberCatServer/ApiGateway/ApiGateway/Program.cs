using System.Security.Cryptography.X509Certificates;
using ApiGateway.Infrastructure;
using ApiGateway.Infrastructure.CompleteTaskWebHookService;
using CommandLine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.Server.Application.Services;
using Shared.Server.Configurations;

var builder = WebApplication.CreateBuilder(args);

var appArgs = Parser.Default.ParseArguments<ApiGatewayArgs>(args).Value;
var host = appArgs.UseHttps ? new Uri("https://0.0.0.0:443") : new Uri("http://0.0.0.0:80");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => { options.TokenValidationParameters = JwtTokenValidation.TokenValidationParameters; });

builder.Services.AddControllers();

// We create a user-friendly widget in Swagger for logging in with a username and password, rather than a JWT token.
builder.Services.AddSwaggerGen(options => { options.AddJwtSecurityDefinition(); });

builder.Services.AddScoped<CompleteTaskWebHookService>();

builder.AddAuthServiceGrpcClient();
builder.AddTaskServiceGrpcClient();
builder.AddPlayerServiceGrpcClient();
builder.AddJudgeServiceGrpcClient();

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

var swaggerProtectPart = "security/yF3bE|TH7~";

app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/" + swaggerProtectPart + "/swagger.{json|yaml}");
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger/" + swaggerProtectPart;
    options.SwaggerEndpoint("/swagger/v1/" + swaggerProtectPart + "/swagger.json", "API");
});

// If we are in development mode (ASPNETCORE_ENVIRONMENT = Development).
if (app.Environment.IsDevelopment())
{
    // We showcase the API specification using Swagger.
    // app.UseSwagger();
    // app.UseSwaggerUI();

    app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger" + "/" + swaggerProtectPart);
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

app.UseMiddleware<ServiceExceptionInterceptorMiddleware>();

// So that the Unity client can access this from a browser (ideally, specific domains need to be configured from which requests can be accepted).
// https://gitlab.com/karim.kimsanbaev/cyber-cat/-/issues/80
app.UseCors("CorsPolicy");
app.UseCors("signalr");

app.MapControllers();

app.Run();