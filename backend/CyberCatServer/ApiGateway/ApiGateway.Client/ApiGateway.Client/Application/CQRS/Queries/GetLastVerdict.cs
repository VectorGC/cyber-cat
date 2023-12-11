using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.CQRS.Queries
{
    public class GetLastVerdict : IQuery<Verdict>, IAuthorizedOnly
    {
        public TaskId TaskId { get; set; }
    }

    public class GetLastVerdictHandler : IQueryHandler<GetLastVerdict, Verdict>
    {
        private readonly IPlayerVerdictHistory _playerVerdictHistory;

        public GetLastVerdictHandler(IPlayerVerdictHistory playerVerdictHistory)
        {
            _playerVerdictHistory = playerVerdictHistory;
        }

        public Task<Verdict> Handle(GetLastVerdict command)
        {
            var verdict = _playerVerdictHistory.GetLastVerdict(command.TaskId);
            return Task.FromResult(verdict);
        }
    }
}