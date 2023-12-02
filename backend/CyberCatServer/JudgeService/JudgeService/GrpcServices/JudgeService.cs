using Shared.Models.Domain.Verdicts;
using Shared.Server.Data;
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

    public async Task<Verdict> GetVerdict(SubmitSolutionArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _taskService.GetTestCases(taskId);
        var testsVerdict = new TestCasesVerdict();

        foreach (var (_, test) in tests.Values)
        {
            var output = await _codeLauncherService.Launch(new LaunchCodeArgs(solution, test.Inputs));
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