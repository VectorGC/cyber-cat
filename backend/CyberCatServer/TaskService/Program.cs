using ProtoBuf.Grpc.Server;
using TaskService;
using TaskService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TaskServiceAppSettings>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<ITaskRepository, TaskRepositoryFromFile>();
builder.Services.AddScoped<ITaskRepository, TaskMongoRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.MapGrpcService<TaskService.Services.TaskGrpcService>();

app.MapControllers();

app.Run();