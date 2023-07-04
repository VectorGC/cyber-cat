using ApiGateway;
using ApiGateway.Extensions;
using AuthService.JwtValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProtoBuf.Grpc.ClientFactory;
using Shared.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Используем строготипизированный класс, чтобы считать appsettings.json и удобно с ним работать.
builder.Services.Configure<ApiGatewayAppSettings>(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { options.TokenValidationParameters = JwtTokenValidation.CreateTokenParameters(); });

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

// "Kestrel:Endpoints:Http:Url" - взятие url для api gateway.
builder.Services.AddSwaggerGen(options => { options.AddJwtSecurityDefinition(builder.Configuration["Kestrel:Endpoints:Http:Url"]); });

var appSettings = builder.Configuration.Get<ApiGatewayAppSettings>();
builder.Services.AddCodeFirstGrpcClient<IAuthGrpcService>(options => { options.Address = appSettings.ConnectionStrings.AuthServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<ITaskGrpcService>(options => { options.Address = appSettings.ConnectionStrings.TaskServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<ISolutionGrpcService>(options => { options.Address = appSettings.ConnectionStrings.SolutionServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<IJudgeGrpcService>(options => { options.Address = appSettings.ConnectionStrings.JudgeServiceGrpcAddress; });

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
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

// Если мы в режиме разработки (ASPNETCORE_ENVIRONMENT = Development).
if (app.Environment.IsDevelopment())
{
    // Показываем API спецификацию через swagger.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        }
    );

    // Подробные ошибки в режиме разработки.
    app.UseDeveloperExceptionPage();
}

// Логирование Http запросов.
app.UseHttpLogging();

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");
app.UseCors("signalr");

// Используем методы контроллеров как ендпоинты.
app.MapControllers();

app.Run();

// Чтобы подцепить сюда тесты.
namespace ApiGateway
{
    internal class Program
    {
    }
}