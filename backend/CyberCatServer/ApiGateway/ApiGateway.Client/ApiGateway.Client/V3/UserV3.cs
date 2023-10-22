using ApiGateway.Client.Internal.WebClientAdapters;
using TinyIoC;

namespace ApiGateway.Client.V3
{
    public class UserV3
    {
        public string Email => Access<CredentialsV3>()?.Email;
        public string Name => Access<CredentialsV3>()?.Name;

        private readonly TinyIoCContainer _container = new TinyIoCContainer();

        public UserV3(ServerEnvironment serverEnvironment)
        {
            var credentials = new CredentialsV3();
            var webClient = new WebClient(serverEnvironment, credentials.AuthorizationTokenHolder);

            _container.Register(credentials);
            _container.Register(webClient);

            _container.Register<VK_V3>();
        }

        public TAccess Access<TAccess>() where TAccess : class, IAccessV3
        {
            if (_container.TryResolve<TAccess>(out var access) && access.IsAvailable)
            {
                return access;
            }

            return default;
        }
    }
}