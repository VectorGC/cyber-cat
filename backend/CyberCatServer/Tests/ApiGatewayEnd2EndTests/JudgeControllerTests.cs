using System.Net.Http.Json;
using ApiGatewayEnd2EndTests.Extensions;
using Shared.Dto;

namespace ApiGatewayEnd2EndTests;

public class JudgeControllerTests
{
    private HttpClient _client;

    [SetUp]
    public async Task SetUp()
    {
        _client = new HttpClient();
        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");
    }

    [Test]
    public async Task SuccessVerifyTutorialTask_WhenPassCorrectCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        var verdictResponse = await _client.PutAsJsonAsync($"http://localhost:5000/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictResponse>();

        Assert.AreEqual(VerdictStatus.Success, verdict.Status);
        Assert.IsNull(verdict.Error);
        Assert.AreEqual("Hello cat!", verdict.Output);
    }

    [Test]
    public async Task FailureVerifyTutorialTask_WhenPassIncorrectCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h> \nint main()";
        var expectedErrorRegex = "Exit Code 1:.*:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

        var verdictResponse = await _client.PutAsJsonAsync($"http://localhost:5000/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictResponse>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
        Assert.IsNull(verdict.Output);
    }

    [Test]
    public async Task FailureVerifyTutorialTask_WhenPassInfinityLoopCode()
    {
        var taskId = "tutorial";
        var sourceCode = "int main() { while(true){} }";

        var verdictResponse = await _client.PutAsJsonAsync($"http://localhost:5000/judge/verify/{taskId}", sourceCode);
        verdictResponse.EnsureSuccessStatusCode();

        var verdict = await verdictResponse.Content.ReadFromJsonAsync<VerdictResponse>();

        Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
        Assert.AreEqual("Exit Code -1: The process took more than 5 seconds", verdict.Error);
        Assert.IsNull(verdict.Output);
    }

    [Test]
    public async Task CompileAndLaunchManyProcess_WithDifferentResult()
    {
        var tasks = new List<Task>();
        for (var i = 0; i < 5; i++)
        {
            tasks.Add(SuccessVerifyTutorialTask_WhenPassCorrectCode());
        }

        for (var i = 0; i < 5; i++)
        {
            tasks.Add(FailureVerifyTutorialTask_WhenPassIncorrectCode());
        }

        for (var i = 0; i < 5; i++)
        {
            tasks.Add(FailureVerifyTutorialTask_WhenPassInfinityLoopCode());
        }

        await Task.WhenAll(tasks);
    }
}