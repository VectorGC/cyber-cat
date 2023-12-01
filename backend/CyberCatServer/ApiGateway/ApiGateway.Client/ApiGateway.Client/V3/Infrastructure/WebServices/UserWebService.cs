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
            catch (WebException e) when (e.Response is HttpWebResponse httpResponse)
            {
                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                        return Result.Failure("User is already registered");
                    default: throw;
                }
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
                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return Result<AuthorizationToken>.Failure($"Not found user '{email}'");
                    case HttpStatusCode.Forbidden:
                        return Result<AuthorizationToken>.Failure("Wrong password");
                    default: throw;
                }
            }
        }

        public UserModel GetUserByToken(AuthorizationToken token)
        {
            if (token is JwtAccessToken jwtAccessToken)
            {
                return new UserModel()
                {
                    Email = jwtAccessToken.email,
                    Name = jwtAccessToken.username,
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