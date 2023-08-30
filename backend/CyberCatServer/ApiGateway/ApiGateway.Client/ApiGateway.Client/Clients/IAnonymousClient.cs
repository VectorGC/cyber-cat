using System.Threading.Tasks;

namespace ApiGateway.Client.Clients
{
    public interface IAnonymousClient
    {
        Task RegisterUser(string email, string password, string name);
        Task<IAuthorizedClient> Authorize(string email, string password);
    }
}