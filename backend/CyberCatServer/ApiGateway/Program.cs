using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    // Подтягиваем в swagger xml комментарии методов.
    // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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