namespace SolutionService.Repositories;

public interface ISolutionRepository
{
    Task<string?> GetSavedCode(string userId, string taskId);
    Task SaveCode(string userId, string taskId, string sourceCode);
    Task RemoveCode(string userId, string taskId);
}