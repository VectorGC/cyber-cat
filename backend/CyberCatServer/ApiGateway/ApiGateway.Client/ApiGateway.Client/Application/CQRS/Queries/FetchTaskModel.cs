using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Domain;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.CQRS.Queries
{
    public class FetchTaskModel : IQuery<TaskModel>
    {
        public TaskId TaskId { get; set; }
        public AuthorizationToken Token { get; set; }
    }

    public class FetchTaskModelHandler : IQueryHandler<FetchTaskModel, TaskModel>
    {
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;
        private readonly ITaskDescriptionRepository _taskDescriptionRepository;

        public FetchTaskModelHandler(WebClientFactory webClientFactory, PlayerContext playerContext, ITaskDescriptionRepository taskDescriptionRepository)
        {
            _taskDescriptionRepository = taskDescriptionRepository;
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
        }

        public async Task<TaskModel> Handle(FetchTaskModel command)
        {
            var taskId = command.TaskId;
            TaskProgress progress = null;
            List<UserModel> usersWhoSolvedTask = null;

            using (var client = _webClientFactory.Create(command.Token))
            {
                progress = await client.GetAsync<TaskProgress>(WebApi.GetTaskProgress(taskId));
                usersWhoSolvedTask = await client.GetAsync<List<UserModel>>(WebApi.GetUsersWhoSolvedTask(taskId));
            }

            if (_playerContext.Player.Tasks.TryGetValue(taskId, out var taskModel))
            {
                taskModel.SetProgress(progress, usersWhoSolvedTask);
            }
            else
            {
                var description = await _taskDescriptionRepository.GetTaskDescription(taskId);
                var testCases = await _taskDescriptionRepository.GetTestCases(taskId);
                _playerContext.Player.Tasks[taskId] = new TaskModel(description, progress, testCases, usersWhoSolvedTask);
            }

            return _playerContext.Player.Tasks[command.TaskId];
        }
    }
}