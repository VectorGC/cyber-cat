using Shared.Dto;
using Shared.Dto.Args;
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

    public async Task<StringProto> GetSavedCode(GetSavedCodeArgs args)
    {
        var solutionCode = await _solutionRepository.GetSavedCode(args.UserId, args.TaskId);
        return solutionCode;
    }

    public async Task SaveCode(SolutionDto dto)
    {
        await _solutionRepository.SaveCode(dto.UserId, dto.TaskId, dto.SourceCode);
    }

    public async Task RemoveCode(RemoveCodeArgs args)
    {
        await _solutionRepository.RemoveCode(args.UserId, args.TaskId);
    }
}