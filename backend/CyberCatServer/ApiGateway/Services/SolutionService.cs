using ApiGateway.Models;
using ApiGateway.Repositories;

namespace ApiGateway.Services;

public class SolutionService : ISolutionService
{
    private readonly ISolutionRepository _solutionRepository;

    public SolutionService(ISolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async Task<string> GetLastSavedCode(UserId userId, string taskId)
    {
        return await _solutionRepository.GetSavedCode(userId, taskId);
    }

    public async Task SaveCode(UserId userId, string taskId, string code)
    {
        await _solutionRepository.SaveCode(taskId, code);
    }
}