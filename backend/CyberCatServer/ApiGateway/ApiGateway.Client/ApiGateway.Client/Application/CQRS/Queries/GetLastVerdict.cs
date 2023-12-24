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
        private readonly IVerdictHistoryService _verdictHistoryService;

        public GetLastVerdictHandler(IVerdictHistoryService verdictHistoryService)
        {
            _verdictHistoryService = verdictHistoryService;
        }

        public Task<Verdict> Handle(GetLastVerdict command)
        {
            var verdict = _verdictHistoryService.GetLastVerdict(command.TaskId);
            return Task.FromResult(verdict);
        }

        public async Task<object> Handle(object command)
        {
            return await Handle(command as GetLastVerdict);
        }
    }
}