using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Application.UseCases.Player;
using ApiGateway.Client.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Users;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.API
{
    public class TaskAPI
    {
        public TaskId Id => _task.Id;
        public TaskDescription Description => _task.Description;
        public List<TestCaseDescription> Tests => _task.TestCases;

        public bool IsComplete => _task.Status.IsComplete;
        public bool IsStarted => _task.Status.IsStarted;
        public string LastSolution => _task.Status.LastSolution;
        public IReadOnlyList<UserModel> UsersWhoSolvedTask => _task.UsersWhoSolvedTask;

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