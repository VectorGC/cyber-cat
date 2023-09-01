using System.Threading.Tasks;

namespace ApiGateway.Client.Clients
{
    public interface IAuthorizedClient
    {
        Task RegisterPlayer();
        Task<IPlayerClient> AuthorizePlayer();
        Task RemoveUser();
    }
}