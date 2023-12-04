using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.Services
{
    public interface ISubmitSolutionTaskService
    {
        Task<Verdict> SubmitSolution(TaskId taskId, string solution, AuthorizationToken token);
    }
}