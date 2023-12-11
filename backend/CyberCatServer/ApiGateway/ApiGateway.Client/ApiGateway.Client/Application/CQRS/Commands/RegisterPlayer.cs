using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class RegisterPlayer : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterPlayerHandler : ICommandHandler<RegisterPlayer>
    {
        private readonly WebClientFactory _webClientFactory;

        public RegisterPlayerHandler(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(RegisterPlayer command)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = command.Email,
                ["password"] = command.Password,
                ["name"] = command.UserName
            };

            using (var client = _webClientFactory.Create())
            {
                await client.PostAsync(WebApi.RegisterPlayer, form);
            }
        }
    }
}