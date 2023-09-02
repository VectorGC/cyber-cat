using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Users;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Anonymous
{
    internal class AnonymousClientProxy : IAnonymous
    {
        private readonly IWebClient _webClient;
        private readonly Uri _uri;

        public AnonymousClientProxy(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
        }

        public async Task SignUp(string email, string password, string name)
        {
            var form = new NameValueCollection()
            {
                ["email"] = email,
                ["password"] = password,
                ["name"] = name,
            };

            await _webClient.PutAsync(_uri + "auth/signUp", form);
        }

        public async Task<IUser> SignIn(string email, string password)
        {
            var token = await GetAuthenticationToken(email, password);
            return new UserClientProxy(token, _uri, _webClient);
        }

        private async Task<string> GetAuthenticationToken(string email, string password)
        {
            var form = new NameValueCollection
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _webClient.PostAsync(_uri + "auth/signIn", form);
            return accessToken;
        }
    }
}