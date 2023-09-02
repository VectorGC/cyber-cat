using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Enums;
using Shared.Server.Dto;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

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

    public async Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _testGrpcService.GetTests(taskId);
        var testPassed = 0;

        foreach (var test in tests)
        {
            var output = await LaunchCode(solution, test.Input);
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

    private async Task<OutputDto> LaunchCode(string solution, string input)
    {
        var output = await _codeLauncherService.Launch(new LaunchCodeArgs(solution, input));
        return output;
    }

    private bool EqualsOutput(string expected, string actual)
    {
        return expected == actual;
    }

    private VerdictData Failure(int testPassed, string error)
    {
        return new VerdictData
        {
            Status = VerdictStatus.Failure,
            Error = error,
            TestsPassed = testPassed
        };
    }

    private VerdictData Success(int testPassed)
    {
        return new VerdictData
        {
            Status = VerdictStatus.Success,
            TestsPassed = testPassed
        };
    }
}