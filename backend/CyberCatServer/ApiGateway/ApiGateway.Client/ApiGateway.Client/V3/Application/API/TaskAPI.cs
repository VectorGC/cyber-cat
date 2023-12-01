using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.UseCases.Player;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.V3.Application.API
{
    public struct TaskAPI
    {
        public TaskId Id => _task.Id;
        public TaskProgressStatus StatusType => _task.ProgressStatus;
        public TaskDescription Description => _task.Description;

        private readonly TaskModel _task;
        private readonly TasksAPI _tasksApi;

        public TaskAPI(TaskModel task, TasksAPI tasksApi)
        {
            _task = task;
            _tasksApi = tasksApi;
        }

        public async Task<Result<Verdict>> SubmitSolution(string solution)
        {
            return await _tasksApi.GetUseCase<SubmitSolution>().Execute(_task.Id, solution);
        }
    }
}