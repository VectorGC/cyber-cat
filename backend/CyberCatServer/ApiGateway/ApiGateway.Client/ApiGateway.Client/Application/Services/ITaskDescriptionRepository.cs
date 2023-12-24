using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace ApiGateway.Client.Application.Services
{
    public interface ITaskDescriptionRepository
    {
        Task<TaskDescription> GetTaskDescription(TaskId taskId);
        Task<Dictionary<TaskId, TaskDescription>> GetAllTaskDescriptions();
        Task<List<TestCaseDescription>> GetTestCases(TaskId taskId);
    }
}