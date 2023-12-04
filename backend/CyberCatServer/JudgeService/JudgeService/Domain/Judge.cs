using Shared.Models.Domain.TestCase;

namespace JudgeService.Domain;

public class Judge
{
    public bool IsSuccess(string output, TestCaseDescription testCase)
    {
        return testCase.Expected == output;
    }
}