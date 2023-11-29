using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class AuthService
    {
        private readonly WebClientFactory _webClientFactory;

        public AuthService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<Result<AuthorizationToken>> LoginPlayer(string email, string password)
        {
            var form = new Dictionary<string, string>
            {
                ["username"] = email,
                ["password"] = password
            };

            try
            {
                using (var client = _webClientFactory.Create())
                {
                    var accessToken = await client.PostAsync<JwtAccessToken>(WebApi.Login, form);
                    return accessToken.Value;
                }
            }
            catch (WebException e)
            {
                return e;
            }
        }

        public async Task<Result> RegisterPlayer(string email, string password, string userName)
        {
            var form = new Dictionary<string, string>()
            {
                ["email"] = email,
                ["password"] = password,
                ["name"] = userName,
            };

            try
            {
                using (var client = _webClientFactory.Create())
                {
                    await client.PostAsync(WebApi.Register, form);
                    return Result.Success;
                }
            }
            catch (WebException e)
            {
                return e;
            }
        }
    }
}