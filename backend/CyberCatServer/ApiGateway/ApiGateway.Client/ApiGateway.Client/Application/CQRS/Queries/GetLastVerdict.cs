using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Application.CQRS.Queries
{
    public class GetLastVerdict : IQuery<Verdict>
    {
        public TaskId TaskId { get; set; }
    }

    public class GetLastVerdictHandler : IQueryHandler<GetLastVerdict, Verdict>
    {
        private readonly IVerdictHistory _verdictHistory;

        public GetLastVerdictHandler(IVerdictHistory verdictHistory)
        {
            _verdictHistory = verdictHistory;
        }

        public Task<Verdict> Handle(GetLastVerdict command)
        {
            var verdict = _verdictHistory.GetLastVerdict(command.TaskId);
            return Task.FromResult(verdict);
        }
    }
}