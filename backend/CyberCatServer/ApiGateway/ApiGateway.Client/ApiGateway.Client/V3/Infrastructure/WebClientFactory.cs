using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class WebClientFactory
    {
        private readonly ServerEnvironment _serverEnvironment;

        public WebClientFactory(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        internal WebClient Create()
        {
            return new WebClient(_serverEnvironment);
        }

        internal WebClient Create(AuthorizationToken token)
        {
            return new WebClient(_serverEnvironment, token);
        }
    }
}