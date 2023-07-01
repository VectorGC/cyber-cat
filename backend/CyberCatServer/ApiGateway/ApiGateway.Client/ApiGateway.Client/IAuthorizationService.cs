using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface IAuthorizationService
    {
        Task<string> Authenticate(string email, string password);
        Task<string> AuthorizePlayer(string token);
    }

    public static class AuthorizationServiceFactory
    {
        public static IAuthorizationService Create(string uri, IRestClient restClient)
        {
            return new Client(uri, restClient);
        }
    }
}