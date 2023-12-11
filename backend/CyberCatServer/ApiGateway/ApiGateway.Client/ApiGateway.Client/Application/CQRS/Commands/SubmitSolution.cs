using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class SubmitSolution : ICommand, IAuthorizedOnly
    {
        public TaskId TaskId { get; set; }
        public string Solution { get; set; }
    }

    public class SubmitSolutionHandler : ICommandHandler<SubmitSolution>
    {
        private readonly IPlayerVerdictHistory _playerVerdictHistory;
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;

        public SubmitSolutionHandler(IPlayerVerdictHistory playerVerdictHistory, WebClientFactory webClientFactory, PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
            _playerVerdictHistory = playerVerdictHistory;
        }

        public async Task Handle(SubmitSolution command)
        {
            using (var client = _webClientFactory.Create(_playerContext.Token))
            {
                var verdict = await client.PostFastJsonAsync<Verdict>(WebApi.SubmitSolution(command.TaskId), new Dictionary<string, string>()
                {
                    ["solution"] = command.Solution
                });

                _playerVerdictHistory.Add(verdict, DateTime.Now);
            }
        }
    }
}