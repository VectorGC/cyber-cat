//using LettuceEncrypt;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddLettuceEncrypt().PersistDataToDirectory(Directory.CreateDirectory("/etc/letsencrypt"), "secret");

var app = builder.Build();

app.UseHttpsRedirection();
//app.UseHsts();

app.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });

app.Run();