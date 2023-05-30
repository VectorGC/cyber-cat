using Shared.Models;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task Add(string taskId, ITest test);
    Task Add(string taskId, IEnumerable<ITest> tests);
    Task<ITests> GetTests(string taskId);
}