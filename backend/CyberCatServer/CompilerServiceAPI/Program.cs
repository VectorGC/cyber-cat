using CompilerServiceAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

builder.Services.AddCompilationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwaggerSwashbuckle();
    app.FallbackToSwaggerPage();

    // ��������� ������ � ������ ����������.
    app.UseDeveloperExceptionPage();
}
app.MapControllers();

app.Run();

