using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using Shared.Models.Domain.Verdicts;
using Shared.Server.ProtoHelpers;

namespace Shared.Server.Services;

[Service]
public interface IJudgeService
{
    Task<Response<Verdict>> GetVerdict(SubmitSolutionArgs args);
}