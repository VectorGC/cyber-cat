using ApiGateway.Service;
using ApiGateway.Service.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProtoBuf.Grpc.ClientFactory;
using Shared.Configurations;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Используем строготипизированный класс, чтобы считать appsettings.json и удобно с ним работать.
builder.Services.Configure<ApiGatewayAppSettings>(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { options.TokenValidationParameters = JwtTokenValidation.CreateTokenParameters(); });

builder.Services.AddControllers();
// "Kestrel:Endpoints:Http:Url" - взятие url для api gateway.
builder.Services.AddSwaggerGen(options => { options.AddJwtSecurityDefinition(builder.Configuration["Kestrel:Endpoints:Http:Url"]); });

var appSettings = builder.Configuration.Get<ApiGatewayAppSettings>();
builder.Services.AddCodeFirstGrpcClient<IAuthGrpcService>(options => { options.Address = appSettings.ConnectionStrings.AuthServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<ITaskGrpcService>(options => { options.Address = appSettings.ConnectionStrings.TaskServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<ISolutionGrpcService>(options => { options.Address = appSettings.ConnectionStrings.SolutionServiceGrpcAddress; });
builder.Services.AddCodeFirstGrpcClient<IJudgeGrpcService>(options => { options.Address = appSettings.ConnectionStrings.JudgeServiceGrpcAddress; });

var app = builder.Build();

// Если мы в режиме разработки (ASPNETCORE_ENVIRONMENT = Development).
if (app.Environment.IsDevelopment())
{
    // Показываем API спецификацию через swagger.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapFallback((context) =>
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

app.UseAuthentication();
app.UseAuthorization();

// Используем методы контроллеров как ендпоинты.
app.MapControllers();

app.Run();

// Чтобы подцепить сюда тесты.
namespace ApiGateway.Service
{
    public partial class Program
    {
    }
}