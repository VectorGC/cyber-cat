using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.API;
using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Application.UseCases;
using ApiGateway.Client.V3.Application.UseCases.Player;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;
using ApiGateway.Client.V3.Infrastructure.WebServices;

namespace ApiGateway.Client.V3.Application
{
    public class ApiGatewayClient : IApiGatewayClient
    {
        public PlayerAPI Player
        {
            get
            {
                var context = _container.Resolve<PlayerContext>();
                return context.IsLogined ? _container.Resolve<PlayerAPI>() : null;
            }
        }

        private readonly TinyIoCContainer _container;

        public ApiGatewayClient(ServerEnvironment serverEnvironment)
        {
            _container = new TinyIoCContainer();

            // --- Application ---
            _container.AutoRegister(type => type.BaseType == typeof(API.API)); // API
            _container.AutoRegister(type => type.GetInterface(nameof(IUseCase)) != null); // UseCases
            _container.Register<PlayerContext>().AsSingleton();

            // --- Infrastructure ---
            _container.Register(new WebClientFactory(serverEnvironment));
            _container.Register<IUserService, UserWebService>().AsSingleton();
            _container.Register<ITaskDescriptionService, TaskDescriptionWebService>().AsSingleton();
            _container.Register<ITaskPlayerProgressService, TaskPlayerProgressWebService>().AsSingleton();
            _container.Register<ISubmitSolutionTaskService, SubmitSolutionTaskWebService>().AsSingleton();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        public Task<Result> RegisterPlayer(string email, string password, string userName) => _container.Resolve<RegisterPlayer>().Execute(email, password, userName);
        public Task<Result<PlayerModel>> LoginPlayer(string email, string password) => _container.Resolve<LoginPlayer>().Execute(email, password);
    }
}