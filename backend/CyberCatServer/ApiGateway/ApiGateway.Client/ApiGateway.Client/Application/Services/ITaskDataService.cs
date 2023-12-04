using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.Services
{
    public interface ITaskDataService
    {
        Task<IReadOnlyList<UserModel>> GetUsersWhoSolvedTask(TaskId taskId, AuthorizationToken token);
    }
}