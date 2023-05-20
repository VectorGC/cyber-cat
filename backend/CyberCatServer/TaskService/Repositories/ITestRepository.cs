using Shared.Models;

namespace TaskService.Repositories;

public interface ITestRepository
{
    Task<ITests?> GetTests(string taskId);
}