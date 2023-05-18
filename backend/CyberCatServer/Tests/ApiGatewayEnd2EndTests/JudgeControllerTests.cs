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
    }

    //[Test]
    public async Task FailureVerifyTutorialTask_WhenPassIncorrectCode()
    {
        Assert.Fail();
    }

    //[Test]
    public async Task FailureVerifyTutorialTask_WhenPassNonCompiledCode()
    {
        Assert.Fail();
    }

    //[Test]
    public async Task FailureVerifyTutorialTask_WhenPassInfinityLoopCode()
    {
        Assert.Fail();
    }
}