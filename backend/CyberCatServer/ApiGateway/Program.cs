var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Если мы в режиме разработки. В Release это работать не будет.
if (app.Environment.IsDevelopment())
{
    // Показываем API спецификацию через swagger.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", (context) =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });

    // Подробные ошибки в режиме разработки.
    app.UseDeveloperExceptionPage();
}

// Размечаем методы контроллеров как ендпоинты.
app.MapControllers();

app.Run();