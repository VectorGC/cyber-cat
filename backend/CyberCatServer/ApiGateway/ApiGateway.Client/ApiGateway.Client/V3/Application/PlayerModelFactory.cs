using System;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Domain;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Application
{
    public class PlayerModelFactory
    {
        private readonly ITaskDescriptionService _taskDescriptionService;
        private readonly ITaskPlayerProgressService _taskProgressService;
        private readonly IUserService _userService;

        public PlayerModelFactory(ITaskDescriptionService taskDescriptionService, ITaskPlayerProgressService taskProgressService, IUserService userService)
        {
            _userService = userService;
            _taskProgressService = taskProgressService;
            _taskDescriptionService = taskDescriptionService;
        }

        public async Task<PlayerModel> Create(AuthorizationToken token)
        {
            var user = _userService.GetUserByToken(token);
            if (!user.Roles.Has(Roles.Player))
                throw new InvalidOperationException("Role player mismatch");

            var tasks = await CreateTaskCollection(token);
            return new PlayerModel(user, tasks);
        }

        private async Task<TaskCollection> CreateTaskCollection(AuthorizationToken token)
        {
            var tasks = new TaskCollection();

            var descriptions = await _taskDescriptionService.GetTaskDescriptions(token);
            var progressDataCollection = await _taskProgressService.GetTasksProgress(token);
            foreach (var description in descriptions)
            {
                var taskProgress = progressDataCollection.FirstOrDefault(data => data.TaskId == description.Id) ?? new TaskProgress();
                tasks[description.Id] = new TaskModel(description, taskProgress);
            }

            return tasks;
        }
    }
}