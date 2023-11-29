using System;
using System.Diagnostics;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application
{
    public class ApiGatewayClient : IDisposable
    {
        public PlayerModel Player
        {
            get
            {
                var playerContext = _container.Resolve<PlayerContext>();
                return playerContext.IsLogined ? playerContext.Player : null;
            }
        }

        public PlayerService PlayerService => _container.Resolve<PlayerService>();
        public TaskService TaskService => _container.Resolve<TaskService>();

        private readonly TinyIoCContainer _container;
        private bool _debug;

        public ApiGatewayClient(ServerEnvironment serverEnvironment)
        {
            _container = new TinyIoCContainer();

            // --- Application ---
            _container.Register<PlayerService>().AsSingleton();
            _container.Register<TaskService>().AsSingleton();
            _container.Register<PlayerContext>().AsSingleton();

            // --- Domain ---
            _container.Register<ITaskRepository, TaskWebRepository>().AsSingleton();

            // --- Infrastructure ---
            SetDebugMode();
            var webClientFactory = new WebClientFactory(serverEnvironment, _debug);
            _container.Register(webClientFactory);
            _container.Register<AuthService>().AsSingleton();
            _container.Register<TaskDescriptionWebProvider>().AsSingleton();
            _container.Register<TaskPlayerProgressWebProvider>().AsSingleton();
        }

        [Conditional("DEBUG")]
        private void SetDebugMode()
        {
            _debug = true;
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}