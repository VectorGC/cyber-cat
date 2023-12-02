using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Verdicts;

namespace Shared.Server.Services;

[Service]
public interface IJudgeService
{
    Task<Verdict> GetVerdict(SubmitSolutionArgs args);
}