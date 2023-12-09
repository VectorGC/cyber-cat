using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.UseCases.Player
{
    public class SubmitSolution : IUseCase
    {
        private readonly PlayerContext _playerContext;
        private readonly IPlayerSubmitSolutionTaskService _playerSubmitSolutionTaskService;
        private readonly ITaskPlayerProgressService _taskPlayerProgressService;
        private readonly ITaskDataService _taskDataService;

        public SubmitSolution(PlayerContext playerContext, IPlayerSubmitSolutionTaskService playerSubmitSolutionTaskService,
            ITaskPlayerProgressService taskPlayerProgressService, ITaskDataService taskDataService)
        {
            _taskDataService = taskDataService;
            _taskPlayerProgressService = taskPlayerProgressService;
            _playerSubmitSolutionTaskService = playerSubmitSolutionTaskService;
            _playerContext = playerContext;
        }

        public async Task<Result<Verdict>> Execute(TaskId taskId, string solution)
        {
            if (!_playerContext.IsLogined)
                return Result<Verdict>.Failure(ErrorCode.NotLoggined);

            var verdict = await _playerSubmitSolutionTaskService.SubmitSolution(taskId, solution, _playerContext.Token);
            var progress = await _taskPlayerProgressService.GetTaskProgress(taskId, _playerContext.Token);
            var usersWhoSolvedTask = await _taskDataService.GetUsersWhoSolvedTask(taskId, _playerContext.Token);

            _playerContext.Player.Tasks[taskId].SetProgress(progress, usersWhoSolvedTask);

            return verdict;
        }
    }
}