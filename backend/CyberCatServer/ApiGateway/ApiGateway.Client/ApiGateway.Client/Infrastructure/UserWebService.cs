using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure
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
            switch (token)
            {
                case JwtAccessToken jwtAccessToken:
                    return new UserModel()
                    {
                        Email = jwtAccessToken.email,
                        FirstName = jwtAccessToken.firstname,
                        Roles = new Roles(jwtAccessToken.roles)
                    };
                case VkAccessToken vkAccessToken:
                    return new UserModel()
                    {
                        Email = vkAccessToken.JwtAccessToken.email,
                        FirstName = vkAccessToken.JwtAccessToken.firstname,
                        Roles = new Roles(vkAccessToken.JwtAccessToken.roles)
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

        public async Task<Result<AuthorizationToken>> LoginWithVk(string email, string userName, string vkId)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["userName"] = userName,
                ["vkId"] = vkId
            };

            try
            {
                using (var client = _webClientFactory.Create())
                {
                    var accessToken = await client.PostFastJsonAsync<VkAccessToken>(WebApi.LoginWithVk, form);
                    return accessToken;
                }
            }
            catch (WebException e) when (e.Response is HttpWebResponse httpResponse)
            {
                return e;
            }
        }

        public Result RemoveUserWithVk(AuthorizationToken token, string vkId)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var result = client.PostAsync(WebApi.RemoveUserWithVk, new Dictionary<string, string>()
                {
                    ["vkId"] = vkId
                }).Result;

                return Result.Success;
            }
        }
    }
}