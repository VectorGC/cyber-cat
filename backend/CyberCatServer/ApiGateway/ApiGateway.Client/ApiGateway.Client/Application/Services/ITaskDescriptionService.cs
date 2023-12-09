using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.UseCases;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace ApiGateway.Client.Application.Services
{
    public interface ITaskDescriptionService
    {
        Task<Result<Dictionary<TaskId, TaskDescription>>> GetTaskDescriptions();
        Task<List<TestCaseDescription>> GetTestCaseDescriptions(TaskId taskId);
    }
}