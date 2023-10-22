using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Users
{
    internal class UserClientProxy : IUser
    {
        private const string AuthenticationScheme = "Bearer";

        private readonly Uri _uri;
        private readonly IWebClientAdapter _webClient;

        public UserClientProxy(string token, Uri uri, IWebClientAdapter webClient)
        {
            _uri = uri;
            _webClient = webClient;
            _webClient.AddAuthorizationHeader(AuthenticationScheme, token);
        }

        public async Task<IPlayer> SignInAsPlayer()
        {
            await _webClient.PostAsync(_uri + "player/signIn");
            return await PlayerClientProxy.Create(_uri, _webClient);
        }

        public async Task Remove(string password)
        {
            var form = new Dictionary<string, string>()
            {
                ["password"] = password
            };

            await _webClient.PostAsync(_uri + "auth/remove", form);
        }
    }
}