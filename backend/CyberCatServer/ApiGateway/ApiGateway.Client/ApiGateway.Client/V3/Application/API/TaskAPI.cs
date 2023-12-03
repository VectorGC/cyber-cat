using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.UseCases.Player;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.V3.Application.API
{
    public struct TaskAPI
    {
        public TaskId Id => _task.Id;
        public TaskDescription Description => _task.Description;
        public List<TestCaseDescription> Tests => _task.TestCases;

        public bool IsComplete => _task.Status.IsComplete;
        public bool IsStarted => _task.Status.IsStarted;
        public string LastSolution => _task.Status.LastSolution;

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