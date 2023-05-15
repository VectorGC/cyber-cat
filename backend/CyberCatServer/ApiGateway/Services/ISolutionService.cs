using ApiGateway.Models;

namespace ApiGateway.Services;

public interface ISolutionService
{
    Task<string> GetLastSavedCode(UserId userId, string taskId);
    Task SaveCode(UserId userId, string taskId, string code);
}