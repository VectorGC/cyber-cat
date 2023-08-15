using System.Threading.Tasks;

namespace ApiGateway.Client.Services
{
    public interface IAuthorizationService
    {
        Task<string> GetAuthenticationToken(string email, string password);
    }
}