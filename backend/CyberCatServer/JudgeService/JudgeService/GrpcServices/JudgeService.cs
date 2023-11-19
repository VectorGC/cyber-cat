using Shared.Models.Enums;
using Shared.Models.Models.Verdicts;
using Shared.Server.Data;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;

namespace JudgeService.GrpcServices;

public class JudgeService : IJudgeService
{
    private readonly ICodeLauncherGrpcService _codeLauncherService;
    private readonly ITaskService _taskService;

    public JudgeService(ICodeLauncherGrpcService codeLauncherService, ITaskService taskService)
    {
        _codeLauncherService = codeLauncherService;
        _taskService = taskService;
    }

    public async Task<Response<Verdict>> GetVerdict(GetVerdictArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _taskService.GetTestCases(taskId);
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
            ? new Success()
            {
                TestCases = testsVerdict
            }
            : new Failure()
            {
                TestCases = testsVerdict,
            };
    }
}