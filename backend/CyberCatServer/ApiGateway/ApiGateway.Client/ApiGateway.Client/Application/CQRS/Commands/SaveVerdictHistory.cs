using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Infrastructure.WebClient;
using fastJSON;
using FluentValidation;
using Shared.Models;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class SaveVerdictHistory : ICommand
    {
        public List<Verdict> Verdicts { get; set; }
        public AuthorizationToken Token { get; set; }
    }

    public class SaveVerdictHistoryValidator : AbstractValidator<SaveVerdictHistory>
    {
        public SaveVerdictHistoryValidator()
        {
            RuleFor(x => x.Verdicts).NotNull().WithErrorCode(ErrorCode.NullVerdictHistory.ToString());
        }
    }

    public class SaveVerdictHistoryHandler : ICommandHandler<SaveVerdictHistory>
    {
        private readonly WebClientFactory _webClientFactory;

        public SaveVerdictHistoryHandler(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(SaveVerdictHistory command)
        {
            using (var client = _webClientFactory.Create(command.Token))
            {
                // Remove constraint to send big data https://support.airship.com/hc/en-us/articles/213492003--Expect-100-Continue-Issues-and-Risks
                client.RemoveHeader("Expect");
                var json = JSON.ToJSON(command.Verdicts, new JSONParameters());
                var zipped = GZIP.ZipToString(json);

                await client.PostAsync(WebApi.SaveVerdictHistory, new Dictionary<string, string>()
                {
                    ["verdictHistoryJson"] = zipped
                });
            }
        }
    }
}