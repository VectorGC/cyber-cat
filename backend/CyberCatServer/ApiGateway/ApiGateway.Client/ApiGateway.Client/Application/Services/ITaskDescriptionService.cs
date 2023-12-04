using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.Services
{
    public interface ITaskDescriptionService
    {
        Task<List<TaskDescription>> GetTaskDescriptions(AuthorizationToken token);
        Task<List<TestCaseDescription>> GetTestCaseDescriptions(TaskId taskId, AuthorizationToken token);
    }
}