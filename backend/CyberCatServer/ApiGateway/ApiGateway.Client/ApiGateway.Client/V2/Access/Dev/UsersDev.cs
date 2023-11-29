using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using Shared.Models;

namespace ApiGateway.Client.V2.Access.Dev
{
    public class UsersDev
    {
        private readonly WebClientV1 _webClientV1;

        internal UsersDev(WebClientV1 webClientV1)
        {
            _webClientV1 = webClientV1;
        }

        public async Task RemoveByEmail(string email)
        {
            var devKeyEncrypted = await Crypto.EncryptAsync("cyber-cat", "cyber");
            await _webClientV1.PostAsync("dev/users/remove", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["devKey"] = devKeyEncrypted
            });
        }
    }
}