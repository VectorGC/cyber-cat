using ApiGateway;
using ApiGateway.BackgroundServices;
using ApiGateway.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<NumberOperationsAggregator>();
builder.Services.AddScoped<SummatorService>();
builder.Services.AddScoped<IMultiplicateService, OtherMultiplicateService>();
builder.Services.AddScoped<ConvertFloatToIntService>();
builder.Services.AddScoped<LoggerForConvertingNumbers>();

builder.Services.AddHostedService<MongoDbConnectionCheck>();

// Users
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCollection, UserCollectionMongoDb>();


var app = builder.Build();

// Секция использования расширений app.Use...
app.UseSwagger();
app.UseSwaggerUI();

// Секция настройки ендпоинты
app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();