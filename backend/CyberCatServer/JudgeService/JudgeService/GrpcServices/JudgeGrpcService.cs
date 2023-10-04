using Shared.Models.Data;
using Shared.Models.Enums;
using Shared.Models.Models;
using Shared.Server.Data;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace JudgeService.GrpcServices;

public class JudgeGrpcService : IJudgeGrpcService
{
    private readonly ICodeLauncherGrpcService _codeLauncherService;
    private readonly ITaskGrpcService _taskGrpcService;

    public JudgeGrpcService(ICodeLauncherGrpcService codeLauncherService, ITaskGrpcService taskGrpcService)
    {
        _codeLauncherService = codeLauncherService;
        _taskGrpcService = taskGrpcService;
    }

    public async Task<Response<VerdictData>> GetVerdict(GetVerdictArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _taskGrpcService.GetTestCases(taskId);
        var testPassed = 0;

        foreach (var (_, test) in tests.Value.Values)
        {
            var output = await LaunchCode(solution, test.Inputs);
            if (!output.Success)
            {
                return Failure(testPassed, output.StandardError);
            }

            var equals = EqualsOutput(test.Expected, output.StandardOutput);
            if (!equals)
            {
                return Failure(testPassed, $"Expected result '{test.Expected}', but was '{output.StandardOutput}'");
            }

            testPassed++;
        }

        return Success(testPassed);
    }

    private bool EqualsOutput(object expected, string actual)
    {
        return Equals(expected, actual);
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

    private async Task<OutputDto> LaunchCode(string solution, string[] inputs)
    {
        var output = await _codeLauncherService.Launch(new LaunchCodeArgs(solution, inputs));
        return output;
    }

    public async Task<Response<VerdictV2>> GetVerdictV2(GetVerdictArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _taskGrpcService.GetTestCases(taskId);
        var testsVerdict = new TestCasesVerdict();

        foreach (var (_, test) in tests.Value.Values)
        {
            var output = (OutputDto) await _codeLauncherService.Launch(new LaunchCodeArgs(solution, test.Inputs));
            if (!output.Success)
            {
                return new NativeFailure()
                {
                    Error = output.StandardError
                };
            }

            var equals = test.Expected == output.StandardOutput;
            if (equals)
                testsVerdict.AddSuccess(test, output.StandardOutput);
            else
                testsVerdict.AddFailure(test, output.StandardOutput);
        }

        return testsVerdict.Values.Values.All(verdict => verdict is SuccessTestCaseVerdict)
            ? new SuccessV2()
            {
                TestCases = testsVerdict
            }
            : new FailureV2()
            {
                TestCases = testsVerdict,
            };
    }
}