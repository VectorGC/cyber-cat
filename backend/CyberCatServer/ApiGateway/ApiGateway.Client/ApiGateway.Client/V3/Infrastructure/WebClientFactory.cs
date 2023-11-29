using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class WebClientFactory
    {
        private readonly ServerEnvironment _serverEnvironment;
        private readonly bool _debug;

        public WebClientFactory(ServerEnvironment serverEnvironment, bool debug)
        {
            _debug = debug;
            _serverEnvironment = serverEnvironment;
        }

        internal WebClient Create()
        {
            return new WebClient(_serverEnvironment, _debug);
        }

        internal WebClient Create(AuthorizationToken token)
        {
            return new WebClient(_serverEnvironment, _debug, token);
        }
    }
}