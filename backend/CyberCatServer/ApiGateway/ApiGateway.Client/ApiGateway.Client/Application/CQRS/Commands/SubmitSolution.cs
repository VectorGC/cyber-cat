using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS.Queries;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Infrastructure.WebClient;
using FluentValidation;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class SubmitSolution : ICommand
    {
        public TaskId TaskId { get; set; }
        public string Solution { get; set; }
        public AuthorizationToken Token { get; set; }
    }

    public class SubmitSolutionValidator : AbstractValidator<SubmitSolution>
    {
        public SubmitSolutionValidator()
        {
            RuleFor(x => x.TaskId).NotNull().WithErrorCode(ErrorCode.TaskIdEmpty.ToString());
            RuleFor(x => x.Solution).NotEmpty().WithErrorCode(ErrorCode.SolutionEmpty.ToString());
        }
    }

    public class SubmitSolutionHandler : ICommandHandler<SubmitSolution>
    {
        private readonly IVerdictHistoryService _verdictHistoryService;
        private readonly WebClientFactory _webClientFactory;
        private readonly IJudgeService _judgeService;
        private readonly Mediator _mediator;

        public SubmitSolutionHandler(IVerdictHistoryService verdictHistoryService, WebClientFactory webClientFactory, IJudgeService judgeService, Mediator mediator)
        {
            _mediator = mediator;
            _judgeService = judgeService;
            _webClientFactory = webClientFactory;
            _verdictHistoryService = verdictHistoryService;
        }

        public async Task Handle(SubmitSolution command)
        {
            if (command.Token == null)
            {
                // Anonymous check.
                var verdict = await _judgeService.GetVerdict(command.TaskId, command.Solution);
                _verdictHistoryService.Add(verdict, DateTime.Now);
            }
            else
            {
                using (var client = _webClientFactory.Create(command.Token))
                {
                    var verdict = await client.PostFastJsonAsync<Verdict>(WebApi.SubmitSolution(command.TaskId), new Dictionary<string, string>()
                    {
                        ["solution"] = command.Solution
                    });

                    _verdictHistoryService.Add(verdict, DateTime.Now);
                }
            }
        }

        public async Task Handle(object command)
        {
            await Handle(command as SubmitSolution);
        }
    }
}