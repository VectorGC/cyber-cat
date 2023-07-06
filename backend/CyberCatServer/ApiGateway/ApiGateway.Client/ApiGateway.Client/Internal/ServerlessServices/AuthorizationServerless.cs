using System.Threading.Tasks;
using ApiGateway.Client.Services.Authorization;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    internal class AuthorizationServerless : IAuthorizationService
    {
        public Task<string> GetAuthenticationToken(string email, string password)
        {
            return Task.FromResult("serverless");
        }
    }
}