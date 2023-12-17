using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;
using ApiGateway.Client.Application.CQRS.Commands;
using ApiGateway.Client.Application.CQRS.Queries;
using ApiGateway.Client.Application.Services;
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
        private readonly Mediator _mediator;
        private readonly PlayerContext _playerContext;

        public TaskAPI(TaskModel task, Mediator mediator, PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _task = task;
            _mediator = mediator;
        }

        public async Task<Result<Verdict>> SubmitSolution(string solution)
        {
            var command = new SubmitSolution()
            {
                TaskId = _task.Id,
                Solution = solution,
                Token = _playerContext.Token
            };
            var result = await _mediator.SendSafe(command);
            if (result != null)
            {
                return Result<Verdict>.FromObject(result);
            }

            var query = new GetLastVerdict()
            {
                TaskId = _task.Id
            };

            // Fetch actual task progress data.
            result = await _mediator.SendSafe(new FetchTaskModel()
            {
                TaskId = _task.Id,
                Token = _playerContext.Token
            });

            var verdict = await _mediator.SendSafe(query);
            return Result<Verdict>.FromObject(verdict);
        }
    }
}