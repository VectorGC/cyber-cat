using System.Diagnostics;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure.WebClient
{
    public class WebClientFactory
    {
        private readonly ServerEnvironment _serverEnvironment;
        private bool _debug;

        public WebClientFactory(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
            SetDebug();
        }

        [Conditional("DEBUG")]
        private void SetDebug()
        {
            _debug = true;
        }

        internal IWebClient Create()
        {
            if (_debug)
                return new WebClientDebugProxy(_serverEnvironment);
            else
                return new WebClient(_serverEnvironment);
        }

        internal IWebClient Create(AuthorizationToken token)
        {
            if (_debug)
                return new WebClientDebugProxy(_serverEnvironment, token);
            else
                return new WebClient(_serverEnvironment, token);
        }
    }
}