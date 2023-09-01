using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using ApiGateway.Client.Internal.ServerlessServices;
using ApiGateway.Client.Services;

namespace ApiGateway.Client.Internal
{
    internal class ServerlessClient : IAnonymousClient, IAuthorizedClient, IPlayerClient
    {
        public ITaskRepository Tasks => new TaskRepositoryServerless();
        public ISolutionService SolutionService => new SolutionServiceServerless();
        public IPlayerService PlayerService => new PlayerServiceServerless();

        public Task<IAuthorizedClient> Authorize(string email, string password)
        {
            return Task.FromResult<IAuthorizedClient>(this);
        }

        public Task RegisterUser(string email, string password, string name)
        {
            return Task.CompletedTask;
        }

        public Task RegisterPlayer()
        {
            return Task.CompletedTask;
        }

        public Task<IPlayerClient> AuthorizePlayer()
        {
            return Task.FromResult<IPlayerClient>(this);
        }

        public Task RemovePlayer()
        {
            return Task.CompletedTask;
        }

        public Task RemoveUser()
        {
            return Task.CompletedTask;
        }
    }
}