using PlayerService.Application;
using PlayerService.Domain;
using PlayerService.Infrastructure;
using ProtoBuf.Grpc.Server;
using Shared.Models.Domain.VerdictHistory;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Domain.Verdicts.TestCases;
using Shared.Server.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddMongoDatabaseContext();
builder.Services.AddScoped<IPlayerRepository, PlayerMongoRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Verdict, VerdictEntity>().ConstructUsing(x => new VerdictEntity(x));
    cfg.CreateMap<VerdictHistory, List<VerdictEntity>>().ConstructUsing((x, context) =>
    {
        var verdicts = x.ToList();
        return context.Mapper.Map<List<VerdictEntity>>(verdicts);
    });
});

builder.AddJudgeServiceGrpcClient();

builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<PlayerGrpcService>();

app.Run();

namespace PlayerService
{
    internal class Program
    {
    }
}