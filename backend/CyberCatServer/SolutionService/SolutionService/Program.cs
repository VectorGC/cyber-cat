using ProtoBuf.Grpc.Server;
using SolutionService;
using SolutionService.Repositories;
using SolutionService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SolutionServiceAppSettings>(builder.Configuration);

builder.Services.AddScoped<ISolutionRepository, SolutionMongoRepository>();

builder.Services.AddCodeFirstGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();

app.MapGrpcService<SolutionGrpcService>();

app.Run();