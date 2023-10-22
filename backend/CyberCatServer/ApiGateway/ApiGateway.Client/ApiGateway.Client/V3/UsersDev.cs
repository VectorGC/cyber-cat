using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using Shared.Models;

namespace ApiGateway.Client.V3
{
    public class UsersDev
    {
        private readonly WebClient _webClient;

        internal UsersDev(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task RemoveByEmail(string email)
        {
            var devKeyEncrypted = await Crypto.EncryptAsync("cyber-cat", "cyber");
            await _webClient.PostAsync("dev/users/remove", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["devKey"] = devKeyEncrypted
            });
        }
    }
}