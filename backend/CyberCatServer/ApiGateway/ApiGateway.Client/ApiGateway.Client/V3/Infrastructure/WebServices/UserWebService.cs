using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure.WebServices
{
    public class UserWebService : IUserService
    {
        private readonly WebClientFactory _webClientFactory;

        public UserWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
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
                    await client.PostAsync(WebApi.RegisterPlayer, form);
                    return Result.Success;
                }
            }
            catch (WebException e)
            {
                return e;
            }
        }

        public async Task<Result<AuthorizationToken>> LoginUser(string email, string password)
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
                    var accessToken = await client.PostFastJsonAsync<JwtAccessToken>(WebApi.Login, form);
                    return accessToken;
                }
            }
            catch (WebException e) when (e.Response is HttpWebResponse httpResponse)
            {
                return e;
            }
        }

        public UserModel GetUserByToken(AuthorizationToken token)
        {
            if (token is JwtAccessToken jwtAccessToken)
            {
                return new UserModel()
                {
                    Email = jwtAccessToken.email,
                    FirstName = jwtAccessToken.firstname,
                    Roles = new Roles(jwtAccessToken.roles)
                };
            }

            return null;
        }

        public Result RemoveUserByToken(AuthorizationToken token, string password)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var result = client.PostAsync(WebApi.RemoveUser, new Dictionary<string, string>()
                {
                    ["password"] = password
                }).Result;
                return Result.Success;
            }
        }
    }
}