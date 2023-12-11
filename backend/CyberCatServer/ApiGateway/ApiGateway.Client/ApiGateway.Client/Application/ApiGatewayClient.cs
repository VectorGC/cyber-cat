using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application.API;
using ApiGateway.Client.Application.CQRS;
using ApiGateway.Client.Application.CQRS.Commands;
using ApiGateway.Client.Application.CQRS.Queries;
using ApiGateway.Client.Application.Middlewares;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Domain;
using ApiGateway.Client.Infrastructure;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Verdicts;

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
        public ITaskDescriptionRepository TaskRepository => _container.Resolve<ITaskDescriptionRepository>();

        private readonly TinyIoCContainer _container;

        public ApiGatewayClient(ServerEnvironment serverEnvironment)
        {
            _container = new TinyIoCContainer();

            // --- Application ---
            _container.Register<PlayerContext>().AsSingleton();

            _container.Register<Mediator>().AsSingleton();
            _container.RegisterCommand<RegisterPlayer, RegisterPlayerHandler>();
            _container.RegisterCommand<LoginPlayer, LoginPlayerHandler>();
            _container.RegisterCommand<LoginPlayerWithVk, LoginPlayerWithVkHandler>();
            _container.RegisterCommand<RemoveCurrentPlayer, RemoveCurrentPlayerHandler>();
            _container.RegisterCommand<LogoutPlayer, LogoutPlayerHandler>();
            _container.RegisterCommand<SubmitSolution, SubmitSolutionHandler>();

            _container.RegisterQuery<GetLastVerdict, GetLastVerdictHandler, Verdict>();
            _container.RegisterQuery<FetchTaskModel, FetchTaskModelHandler, TaskModel>();
            _container.RegisterQuery<FetchTaskCollection, FetchTaskCollectionHandler, TaskCollection>();

            _container.UseMiddleware<CatchExceptionMiddleware>();
            _container.UseMiddleware<AnonymousGuardMiddleware>();
            _container.UseMiddleware<AuthorizedGuardMiddleware>();

            // --- Infrastructure ---
            _container.Register(new WebClientFactory(serverEnvironment));
            _container.Register<ITaskDescriptionRepository, TaskDescriptionWebRepository>().AsSingleton();
            _container.Register<IJudgeService, JudgeWebService>().AsSingleton();
            _container.Register<IPlayerVerdictHistory, PlayerVerdictHistory>().AsSingleton();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        public async Task<Result> RegisterPlayer(string email, string password, string userName)
        {
            var mediator = _container.Resolve<Mediator>();

            var command = new RegisterPlayer()
            {
                Email = email,
                Password = password,
                UserName = userName
            };

            var result = await mediator.SendSafe(command);
            return Result.FromObject(result);
        }

        public async Task<Result<PlayerModel>> LoginPlayer(string email, string password)
        {
            var mediator = _container.Resolve<Mediator>();

            var command = new LoginPlayer()
            {
                Email = email,
                Password = password
            };

            var result = await mediator.SendSafe(command);
            if (result == null)
            {
                var playerContext = _container.Resolve<PlayerContext>();
                return playerContext.Player;
            }

            return Result<PlayerModel>.FromObject(result);
        }

        public async Task<Result<PlayerModel>> LoginPlayerWithVk(string email, string userName, string vkId)
        {
            var mediator = _container.Resolve<Mediator>();

            var command = new LoginPlayerWithVk()
            {
                Email = email,
                UserName = userName,
                VkId = vkId
            };

            var result = await mediator.SendSafe(command);
            if (result == null)
            {
                var playerContext = _container.Resolve<PlayerContext>();
                return playerContext.Player;
            }

            return Result<PlayerModel>.FromObject(result);
        }
    }
}