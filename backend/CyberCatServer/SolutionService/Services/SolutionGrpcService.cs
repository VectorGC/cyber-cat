using Shared.Dto;
using Shared.Services;
using SolutionService.Repositories;

namespace SolutionService.Services;

public class SolutionGrpcService : ISolutionGrpcService
{
    private readonly ISolutionRepository _solutionRepository;

    public SolutionGrpcService(ISolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async Task<GetSavedCodeResponse> GetSavedCode(GetSavedCodeArgs args)
    {
        var solutionCode = await _solutionRepository.GetSavedCode(args.UserId, args.TaskId);
        return new GetSavedCodeResponse
        {
            SolutionCode = solutionCode ?? string.Empty
        };
    }

    public async Task SaveCode(SolutionArgs args)
    {
        await _solutionRepository.SaveCode(args.UserId, args.TaskId, args.SolutionCode);
    }

    public async Task RemoveCode(RemoveCodeArgs args)
    {
        await _solutionRepository.RemoveCode(args.UserId, args.TaskId);
    }
}