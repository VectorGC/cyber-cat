using System.Threading.Tasks;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Ids;

namespace ApiGateway.Client.V3.Application
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly SubmitSolutionTaskService _submitSolutionTaskService;
        private readonly TaskPlayerProgressWebProvider _taskPlayerProgressProvider;
        private readonly PlayerContext _playerContext;

        public TaskService(PlayerContext playerContext, ITaskRepository taskRepository, SubmitSolutionTaskService submitSolutionTaskService, TaskPlayerProgressWebProvider taskPlayerProgressProvider)
        {
            _playerContext = playerContext;
            _taskPlayerProgressProvider = taskPlayerProgressProvider;
            _submitSolutionTaskService = submitSolutionTaskService;
            _taskRepository = taskRepository;
        }

        public async Task<Result<Verdict>> SubmitSolution(TaskId taskId, string solution)
        {
            if (!_playerContext.IsLogined)
                return "User forbidden";

            var verdict = await _submitSolutionTaskService.VerifySolution(taskId, solution, _playerContext.Token);
            var progress = await _taskPlayerProgressProvider.GetTaskProgress(taskId, _playerContext.Token);
            _taskRepository[verdict.TaskId].SetProgress(progress);

            return verdict;
        }
    }
}