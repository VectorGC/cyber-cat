using Shared.Models.Ids;
using Shared.Models.Models.TestCases;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task<TestCases> GetTestCases(TaskId id);
}