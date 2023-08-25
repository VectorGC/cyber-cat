using ApiGateway.Client.Internal.ServerlessServices;
using ApiGateway.Client.Services;

namespace ApiGateway.Client.Internal
{
    internal class ServerlessClient : IAnonymousClient, IAuthorizedClient
    {
        public IAuthorizationService Authorization => new AuthorizationServerless();
        public ITaskRepository Tasks => new TaskRepositoryServerless();
        public ISolutionService SolutionService => new SolutionServiceServerless();
        public IJudgeService JudgeService => new JudgeServiceServerless();
        public IPlayerService PlayerService { get; }
    }
}