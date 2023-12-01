using Shared.Models.Domain.Tasks;
using Shared.Models.Models.TestCases;

namespace TaskService.Application;

public interface ITestRepository
{
    Task<TestCases> GetTestCases(TaskId id);
}