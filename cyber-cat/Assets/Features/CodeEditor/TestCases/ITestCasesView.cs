using ApiGateway.Client.Models;

public interface ITestCasesView
{
    TestCases TestCases { get; set; }
    TestCasesVerdict TestCasesVerdict { get; set; }
}