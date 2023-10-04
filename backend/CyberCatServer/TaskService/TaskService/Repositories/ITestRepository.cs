using Shared.Models.Ids;
using Shared.Models.Models;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task Add(TestCases tests);
    Task<TestCases> GetTestCases(TaskId taskId);
}