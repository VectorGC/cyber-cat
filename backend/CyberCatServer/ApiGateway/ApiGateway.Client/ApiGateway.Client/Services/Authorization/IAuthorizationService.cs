using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface IAuthorizationService
    {
        Task<string> GetAuthenticationToken(string email, string password);
    }
}