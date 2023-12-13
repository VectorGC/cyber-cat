using System.Collections.Generic;
using System.Linq;
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
    public class FetchTaskCollection : IQuery<TaskCollection>
    {
        public AuthorizationToken Token { get; set; }
    }

    public class FetchTaskCollectionHandler : IQueryHandler<FetchTaskCollection, TaskCollection>, IAuthorizedOnly
    {
        private readonly ITaskDescriptionRepository _taskDescriptionRepository;
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;

        public FetchTaskCollectionHandler(WebClientFactory webClientFactory, PlayerContext playerContext, ITaskDescriptionRepository taskDescriptionRepository)
        {
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
            _taskDescriptionRepository = taskDescriptionRepository;
        }

        public async Task<TaskCollection> Handle(FetchTaskCollection command)
        {
            var tasks = new TaskCollection();

            List<TaskProgress> progresses;
            using (var client = _webClientFactory.Create(command.Token))
            {
                progresses = await client.GetFastJsonAsync<List<TaskProgress>>(WebApi.GetTasksProgress);
            }

            var descriptions = await _taskDescriptionRepository.GetAllTaskDescriptions();
            foreach (var description in descriptions.Values)
            {
                var testCases = await _taskDescriptionRepository.GetTestCases(description.Id);
                var taskProgress = progresses.FirstOrDefault(data => data.TaskId == description.Id) ?? new TaskProgress()
                {
                    TaskId = description.Id
                };

                List<UserModel> usersWhoSolvedTask;
                using (var client = _webClientFactory.Create(command.Token))
                {
                    usersWhoSolvedTask = await client.GetFastJsonAsync<List<UserModel>>(WebApi.GetUsersWhoSolvedTask(description.Id));
                }


                tasks[description.Id] = new TaskModel(description, taskProgress, testCases, usersWhoSolvedTask);
            }

            return tasks;
        }
    }
}