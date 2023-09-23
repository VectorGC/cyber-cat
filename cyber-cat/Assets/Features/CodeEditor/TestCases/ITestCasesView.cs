using ApiGateway.Client.Models;

public interface ITestCasesView
{
    void Show(TestCases testCases);
    void Show(TestCasesVerdict testCasesVerdict);
    void Hide();
}