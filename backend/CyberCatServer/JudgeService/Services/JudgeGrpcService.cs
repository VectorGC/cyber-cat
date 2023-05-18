using Shared.Dto;
using Shared.Services;

namespace JudgeService.Services;

public class JudgeGrpcService : IJudgeGrpcService
{
    private readonly ICodeLauncherGrpcService _codeLauncherGrpcService;

    public JudgeGrpcService(ICodeLauncherGrpcService codeLauncherGrpcService)
    {
        _codeLauncherGrpcService = codeLauncherGrpcService;
    }

    public async Task<VerdictResponse> GetVerdict(SolutionArgs args)
    {
        var launchCodeArgs = new SourceCodeArgs
        {
            SourceCode = args.SolutionCode
        };

        var response = await _codeLauncherGrpcService.Launch(launchCodeArgs);
        if (response.HasError)
        {
            return new VerdictResponse
            {
                Status = VerdictStatus.Failure,
                Error = response.StandardError
            };
        }

        return new VerdictResponse
        {
            Status = VerdictStatus.Success,
            Output = response.StandardOutput
        };
    }
}