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
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application
{
    public class ApiGatewayClient : IDisposable
    {
        public PlayerAPI Player => _playerApi;

        public IVerdictHistoryService VerdictHistoryService => _container.Resolve<IVerdictHistoryService>();
        public ITaskDescriptionRepository TaskRepository => _container.Resolve<ITaskDescriptionRepository>();

        private readonly TinyIoCContainer _container;
        private PlayerAPI _playerApi;

        public ApiGatewayClient(ServerEnvironment serverEnvironment)
        {
            _container = new TinyIoCContainer();

            // --- Application ---
            _container.Register(this);
            _container.Register<PlayerContext>().AsSingleton();

            _container.Register<Mediator>().AsSingleton();
            _container.RegisterCommand<RegisterPlayer, RegisterPlayerHandler, RegisterPlayerValidator>();
            _container.RegisterCommand<LoginPlayer, LoginPlayerHandler, LoginPlayerValidator>();
            _container.RegisterCommand<LoginPlayerWithVk, LoginPlayerWithVkHandler>();
            _container.RegisterCommand<RemoveCurrentPlayer, RemoveCurrentPlayerHandler>();
            _container.RegisterCommand<LogoutPlayer, LogoutPlayerHandler>();
            _container.RegisterCommand<SubmitSolution, SubmitSolutionHandler, SubmitSolutionValidator>();
            _container.RegisterCommand<SaveVerdictHistory, SaveVerdictHistoryHandler, SaveVerdictHistoryValidator>();

            _container.RegisterQuery<GetLastVerdict, GetLastVerdictHandler, Verdict>();
            _container.RegisterQuery<FetchTaskModel, FetchTaskModelHandler, TaskModel>();
            _container.RegisterQuery<FetchTaskCollection, FetchTaskCollectionHandler, TaskCollection>();

            _container.UseMiddleware<CatchExceptionMiddleware>();
            _container.UseMiddleware<AnonymousGuardMiddleware>();
            _container.UseMiddleware<AuthorizedGuardMiddleware>();
            _container.UseMiddleware<FluentValidatorMiddleware>();

            // --- Infrastructure ---
            _container.Register(new WebClientFactory(serverEnvironment));
            _container.Register<ITaskDescriptionRepository, TaskDescriptionWebRepository>().AsSingleton();
            _container.Register<IJudgeService, JudgeWebService>().AsSingleton();
            _container.Register<IVerdictHistoryService, VerdictHistoryService>().AsSingleton();
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
                _playerApi = _container.Resolve<PlayerAPI>();
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
                _playerApi = _container.Resolve<PlayerAPI>();
                return playerContext.Player;
            }

            return Result<PlayerModel>.FromObject(result);
        }

        public async Task<Result> Logout()
        {
            var mediator = _container.Resolve<Mediator>();

            var command = new LogoutPlayer();
            var result = await mediator.SendSafe(command);
            var res = Result.FromObject(result);
            if (res.IsSuccess)
            {
                _playerApi = null;
            }

            return res;
        }

        public async Task<Result> Remove()
        {
            var mediator = _container.Resolve<Mediator>();

            var command = new RemoveCurrentPlayer();
            var result = await mediator.SendSafe(command);
            var res = Result.FromObject(result);
            if (res.IsSuccess)
            {
                _playerApi = null;
            }

            return res;
        }

        public async Task<Result<Verdict>> SubmitAnonymousSolution(TaskId taskId, string solution)
        {
            var mediator = _container.Resolve<Mediator>();
            var command = new SubmitSolution()
            {
                TaskId = taskId,
                Solution = solution,
            };
            var result = await mediator.SendSafe(command);
            if (result != null)
            {
                return Result<Verdict>.FromObject(result);
            }

            var query = new GetLastVerdict()
            {
                TaskId = taskId
            };

            var verdict = await mediator.SendSafe(query);
            return Result<Verdict>.FromObject(verdict);
        }

        public async Task<Result> SaveVerdictHistory(AuthorizationToken token)
        {
            var mediator = _container.Resolve<Mediator>();
            var verdicts = VerdictHistoryService.GetAll();
            var command = new SaveVerdictHistory()
            {
                Verdicts = verdicts,
                Token = token
            };
            var result = await mediator.SendSafe(command);
            return Result.FromObject(result);
        }
    }
}