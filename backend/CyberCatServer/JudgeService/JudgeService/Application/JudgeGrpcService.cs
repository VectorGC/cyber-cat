using Shared.Models.Domain.Verdicts;
using Shared.Server.Services;

namespace JudgeService.Application;

public class JudgeGrpcService : IJudgeService
{
    private readonly ICodeLauncherService _codeLauncherService;
    private readonly ITaskService _taskService;

    public JudgeGrpcService(ICodeLauncherService codeLauncherService, ITaskService taskService)
    {
        _codeLauncherService = codeLauncherService;
        _taskService = taskService;
    }

    public async Task<Verdict> GetVerdict(SubmitSolutionArgs args)
    {
        var (_, taskId, solution) = args;
        var tests = await _taskService.GetTestCases(taskId);
        var testsVerdict = new TestCasesVerdict();

        foreach (var test in tests)
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