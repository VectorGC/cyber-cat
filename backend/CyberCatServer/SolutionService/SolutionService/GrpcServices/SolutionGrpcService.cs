using Shared.Dto.Args;
using Shared.Dto.ProtoHelpers;
using Shared.Services;
using SolutionService.Repositories;

namespace SolutionService.GrpcServices;

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

    public async Task SaveCode(SaveCodeArgs args)
    {
        await _solutionRepository.Save(args.UserId, args.Solution);
    }

    public async Task RemoveCode(RemoveCodeArgs args)
    {
        await _solutionRepository.RemoveCode(args.UserId, args.TaskId);
    }
}