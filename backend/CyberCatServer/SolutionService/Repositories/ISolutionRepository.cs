using Shared.Models;

namespace SolutionService.Repositories;

public interface ISolutionRepository
{
    Task<string?> GetSavedCode(string userId, string taskId);
    Task Save(ISolution solution);
    Task RemoveCode(string userId, string taskId);
}