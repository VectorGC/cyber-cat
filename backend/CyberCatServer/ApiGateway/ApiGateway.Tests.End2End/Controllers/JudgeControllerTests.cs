using System.Net.Http.Json;
using ApiGateway.Tests.End2End.Extensions;
using Shared.Dto;
using Shared.Models;

namespace ApiGateway.Tests.End2End.Controllers;

[TestFixture]
public class JudgeControllerTests : E2ETests
{
    [Test]
    public async Task SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        var verdictResponse = await Client.PutAsJsonAsync($"/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictDto>();

        Assert.AreEqual(VerdictStatus.Success, verdict.Status);
        Assert.IsNull(verdict.Error);
        Assert.AreEqual(1, verdict.TestsPassed);
    }

    [Test]
    public async Task FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h> \nint main()";
        var expectedErrorRegex = "Exit Code 1:.*:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

        var verdictResponse = await Client.PutAsJsonAsync($"/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictDto>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.AreEqual(0, verdict.TestsPassed);
        Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
    }

    [Test]
    public async Task FailureVerifyHelloCatTaskWithError_WhenPassInfinityLoopCode()
    {
        var taskId = "tutorial";
        var sourceCode = "int main() { while(true){} }";

        var verdictResponse = await Client.PutAsJsonAsync($"/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictDto>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.AreEqual(0, verdict.TestsPassed);
        Assert.AreEqual("Exit Code -1: The process took more than 5 seconds", verdict.Error);
    }

    [Test]
    public async Task CompileAndLaunchManyProcess_WithDifferentResult()
    {
        var tasks = new List<Task>();
        for (var i = 0; i < 5; i++)
        {
            tasks.Add(SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode());
        }

        for (var i = 0; i < 5; i++)
        {
            tasks.Add(FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode());
        }

        for (var i = 0; i < 5; i++)
        {
            tasks.Add(FailureVerifyHelloCatTaskWithError_WhenPassInfinityLoopCode());
        }

        await Task.WhenAll(tasks);
    }

    [Test]
    public async Task FailureWithInput_WhenPassNotAllTests()
    {
        const string taskId = "sum_ab";
        // Просто выводим результат первого теста. Чтобы первый тест прошел, а остальные завалились.
        const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; scanf(\"%d%d\", &a, &b); printf(\"2\"); }";

        var verdictResponse = await Client.PutAsJsonAsync($"/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictDto>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.AreEqual(1, verdict.TestsPassed);
        Assert.AreEqual("Expected result '15', but was '2'", verdict.Error);
    }

    [Test]
    public async Task Failure_WhenWaitInputInfinity()
    {
        const string taskId = "sum_ab";
        // Сделали лишний ввод, бесконечно ждем, когда введется 'c'.
        const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; int c; scanf(\"%d%d\", &a, &b); scanf(\"%d\", &c); }";

        var verdictResponse = await Client.PutAsJsonAsync($"/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictDto>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.AreEqual(0, verdict.TestsPassed);
        Assert.AreEqual("Exit Code -1: The process took more than 5 seconds", verdict.Error);
    }
}