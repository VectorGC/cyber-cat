using System.Threading.Tasks;

namespace ApiGateway.Client.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<string> GetAuthenticationToken(string email, string password);
    }
}