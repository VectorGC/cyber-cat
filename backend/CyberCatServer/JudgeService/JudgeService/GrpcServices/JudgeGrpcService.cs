using Shared.Dto;
using Shared.Dto.Args;
using Shared.Models;
using Shared.Services;

namespace JudgeService.GrpcServices;

public class JudgeGrpcService : IJudgeGrpcService
{
    private readonly ICodeLauncherGrpcService _codeLauncherService;
    private readonly ITestGrpcService _testGrpcService;

    public JudgeGrpcService(ICodeLauncherGrpcService codeLauncherService, ITestGrpcService testGrpcService)
    {
        _codeLauncherService = codeLauncherService;
        _testGrpcService = testGrpcService;
    }

    public async Task<VerdictDto> GetVerdict(SolutionDto solution)
    {
        var tests = await _testGrpcService.GetTests(solution.TaskId);
        var testPassed = 0;

        foreach (var test in tests)
        {
            var output = await LaunchCode(solution.SourceCode, test.Input);
            if (!output.Success)
            {
                return Failure(testPassed, output.StandardError);
            }

            var equals = EqualsOutput(test.ExpectedOutput, output.StandardOutput);
            if (!equals)
            {
                return Failure(testPassed, $"Expected result '{test.ExpectedOutput}', but was '{output.StandardOutput}'");
            }

            testPassed++;
        }

        return Success(testPassed);
    }

    private async Task<OutputDto> LaunchCode(string sourceCode, string input)
    {
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = input
        };

        var output = await _codeLauncherService.Launch(args);
        return output;
    }

    private bool EqualsOutput(string expected, string actual)
    {
        return expected == actual;
    }

    private VerdictDto Failure(int testPassed, string error)
    {
        return new VerdictDto
        {
            Status = VerdictStatus.Failure,
            Error = error,
            TestsPassed = testPassed
        };
    }

    private VerdictDto Success(int testPassed)
    {
        return new VerdictDto
        {
            Status = VerdictStatus.Success,
            TestsPassed = testPassed
        };
    }
}