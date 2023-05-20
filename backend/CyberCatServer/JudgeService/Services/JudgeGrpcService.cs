using Shared.Dto;
using Shared.Dto.Args;
using Shared.Models;
using Shared.Services;

namespace JudgeService.Services;

public class JudgeGrpcService : IJudgeGrpcService
{
    private readonly ICodeLauncherGrpcService _codeLauncherService;
    private readonly ITestGrpcService _testService;

    public JudgeGrpcService(ICodeLauncherGrpcService codeLauncherService, ITestGrpcService testService)
    {
        _codeLauncherService = codeLauncherService;
        _testService = testService;
    }

    // TODO: Здесь не нужен UserId.
    public async Task<VerdictResponse> GetVerdict(SolutionDto solution)
    {
        var tests = await _testService.GetTests(solution.TaskId);
        var testPassed = 0;

        // TODO: Нужна коллекция и удобная итерация по объектам в коллекции.
        foreach (var test in tests.Tests)
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

    // TODO: Output как будто отдельная бизнес сущность
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

    private VerdictResponse Failure(int testPassed, string error)
    {
        return new VerdictResponse
        {
            Status = VerdictStatus.Failure,
            Error = error,
            TestsPassed = testPassed
        };
    }

    private VerdictResponse Success(int testPassed)
    {
        return new VerdictResponse
        {
            Status = VerdictStatus.Success,
            TestsPassed = testPassed
        };
    }
}