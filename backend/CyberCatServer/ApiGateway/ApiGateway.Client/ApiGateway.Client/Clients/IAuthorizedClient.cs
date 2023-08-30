using System.Threading.Tasks;

namespace ApiGateway.Client.Clients
{
    public interface IAuthorizedClient
    {
        Task<IPlayerClient> AuthorizePlayer();
        Task RemoveUser();
    }
}