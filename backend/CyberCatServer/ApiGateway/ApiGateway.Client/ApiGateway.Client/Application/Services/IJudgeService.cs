using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.Services
{
    public interface IJudgeService
    {
        Task<Verdict> GetVerdict(TaskId taskId, string solution);
    }
}