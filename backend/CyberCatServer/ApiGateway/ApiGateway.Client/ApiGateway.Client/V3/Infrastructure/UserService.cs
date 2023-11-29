using System.Threading.Tasks;
using ApiGateway.Client.V3.Application;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class UserService
    {
        private readonly WebClientFactory _webClientFactory;

        public UserService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<UserModel> GetUserByToken(AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var user = await client.GetAsync<UserModel>(WebApi.GetCurrentUserData);
                return user;
            }
        }

        public Result RemoveUserByToken(AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var result = client.PostAsync(WebApi.GetCurrentUserData).Result;
                return Result.Success;
            }
        }
    }
}