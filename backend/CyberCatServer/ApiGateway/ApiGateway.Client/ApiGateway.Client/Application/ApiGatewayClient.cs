using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application.API;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Application.UseCases.Player;
using ApiGateway.Client.Application.UseCases.Vk;
using ApiGateway.Client.Domain;
using ApiGateway.Client.Infrastructure;
using ApiGateway.Client.Infrastructure.WebClient;

namespace ApiGateway.Client.Application
{
    public class ApiGatewayClient : IDisposable
    {
        public PlayerAPI Player
        {
            get
            {
                var context = _container.Resolve<PlayerContext>();
                return context.IsLogined ? _container.Resolve<PlayerAPI>() : null;
            }
        }

        public IJudgeService JudgeService => _container.Resolve<IJudgeService>();
        public ITaskDescriptionService TaskService => _container.Resolve<ITaskDescriptionService>();

        private readonly TinyIoCContainer _container;

        public ApiGatewayClient(ServerEnvironment serverEnvironment)
        {
            _container = new TinyIoCContainer();

            // --- Application ---
            _container.AutoRegister(type => type.BaseType == typeof(UseCaseAPI)); // API
            _container.AutoRegister(type => type.GetInterface(nameof(IUseCase)) != null); // UseCases
            _container.Register<PlayerContext>().AsSingleton();

            // --- Infrastructure ---
            _container.Register(new WebClientFactory(serverEnvironment));
            _container.Register<IUserService, UserWebService>().AsSingleton();
            _container.Register<ITaskDescriptionService, TaskDescriptionWebService>().AsSingleton();
            _container.Register<ITaskPlayerProgressService, TaskPlayerProgressWebService>().AsSingleton();
            _container.Register<ITaskDataService, TaskDataWebService>().AsSingleton();
            _container.Register<IPlayerSubmitSolutionTaskService, PlayerSubmitSolutionTaskWebService>().AsSingleton();
            _container.Register<IJudgeService, JudgeWebService>().AsSingleton();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        public Task<Result> RegisterPlayer(string email, string password, string userName) => _container.Resolve<RegisterPlayer>().Execute(email, password, userName);
        public Task<Result<PlayerModel>> LoginPlayer(string email, string password) => _container.Resolve<LoginPlayer>().Execute(email, password);
        public Task<Result<PlayerModel>> LoginPlayerWithVk(string email, string userName, string vkId) => _container.Resolve<LoginWithVk>().Execute(email, userName, vkId);
    }
}