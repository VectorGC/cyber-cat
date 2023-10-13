using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.V2
{
    public class Dev : IAccess
    {
        private readonly IWebClient _webClient = WebClientFactory.Create();
        private readonly ServerEnvironment _serverEnvironment;

        public Dev(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        public async Task RemoveUser(string userEmail)
        {
            var webClient = WebClientFactory.Create();
            await webClient.PostAsync(_serverEnvironment.GetUri() + "auth/dev/remove", new Dictionary<string, string>()
            {
                ["userEmail"] = userEmail,
                ["key"] = "cyber"
            });
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}