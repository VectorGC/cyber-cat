using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.V2.Access;
using TinyIoC;

namespace ApiGateway.Client.V2
{
    public class User
    {
        public string Email => Access<Credentials>()?.Email;
        public string Name => Access<Credentials>()?.Name;

        private readonly TinyIoCContainer _container = new TinyIoCContainer();

        public User(ServerEnvironment serverEnvironment)
        {
            var credentials = new Credentials();
            var webClient = new WebClient(serverEnvironment, credentials.AuthorizationTokenHolder);

            _container.Register(credentials);
            _container.Register(webClient);

            _container.Register<Vk>();
        }

        public TAccess Access<TAccess>() where TAccess : class, IAccess
        {
            if (_container.TryResolve<TAccess>(out var access) && access.IsAvailable)
            {
                return access;
            }

            return default;
        }
    }
}