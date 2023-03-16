using ApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Services.AddUserService();
builder.Services.AddAuthUserService();

var app = builder.Build();

// Если мы в режиме разработки. В Release это работать не будет. Пока делаем поведение одинаковым везде.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwaggerSwashbuckle();
    app.FallbackToSwaggerPage();

    // Подробные ошибки в режиме разработки.
    app.UseDeveloperExceptionPage();
}

// Размечаем методы контроллеров как ендпоинты.
app.MapControllers();

app.Run();