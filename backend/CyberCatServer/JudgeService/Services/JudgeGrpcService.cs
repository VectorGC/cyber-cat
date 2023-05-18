using Shared.Dto;
using Shared.Services;

namespace JudgeService.Services;

public class JudgeGrpcService : IJudgeGrpcService
{
    public async Task<VerdictResponse> GetVerdict(SolutionArgs args)
    {
        return new VerdictResponse
        {
            Status = VerdictStatus.Success
        };
    }
}