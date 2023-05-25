using Shared.Models;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task<List<ITest>?> GetTests(string taskId);
}