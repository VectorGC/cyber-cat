using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Application.Services
{
    public interface ITaskPlayerProgressService
    {
        Task<TaskProgress> GetTaskProgress(TaskId taskId, AuthorizationToken token);
        Task<List<TaskProgress>> GetTasksProgress(AuthorizationToken token);
    }
}