using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.V2
{
    public class VK : IAccess
    {
        public bool IsSignedIn { get; private set; }
        public string FirstName { get; private set; }
        public string Email { get; private set; }

        private readonly IWebClient _webClient = WebClientFactory.Create();
        private readonly ServerEnvironment _serverEnvironment;

        public VK(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        public async Task<bool> SignIn(string email, string userName)
        {
            var uri = _serverEnvironment.GetUri();
            await _webClient.PostAsync(uri + "Vk/signIn", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["name"] = userName
            });

            FirstName = userName;
            Email = email;
            IsSignedIn = true;

            return true;
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}