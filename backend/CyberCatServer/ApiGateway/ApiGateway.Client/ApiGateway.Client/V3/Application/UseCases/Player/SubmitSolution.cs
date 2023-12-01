using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.V3.Application.UseCases.Player
{
    public class SubmitSolution : IUseCase
    {
        private readonly PlayerContext _playerContext;
        private readonly ISubmitSolutionTaskService _submitSolutionTaskService;
        private readonly ITaskPlayerProgressService _taskPlayerProgressService;

        public SubmitSolution(PlayerContext playerContext, ISubmitSolutionTaskService submitSolutionTaskService, ITaskPlayerProgressService taskPlayerProgressService)
        {
            _taskPlayerProgressService = taskPlayerProgressService;
            _submitSolutionTaskService = submitSolutionTaskService;
            _playerContext = playerContext;
        }

        public async Task<Result<Verdict>> Execute(TaskId taskId, string solution)
        {
            if (!_playerContext.IsLogined)
                return Result<Verdict>.Failure("User forbidden");

            var verdict = await _submitSolutionTaskService.SubmitSolution(taskId, solution, _playerContext.Token);
            var progress = await _taskPlayerProgressService.GetTaskProgress(taskId, _playerContext.Token);
            _playerContext.Player.Tasks[taskId].SetProgress(progress);

            return verdict;
        }
    }
}