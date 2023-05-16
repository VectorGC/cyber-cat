using System.Net;
using System.Net.Http.Json;
using ApiGateway.Dto;
using ApiGatewayEnd2EndTests.Extensions;
using Shared.Dto;

namespace ApiGatewayEnd2EndTests;

public class SolutionControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        _client = new HttpClient();
    }

    [Test]
    public async Task SaveCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        var args = new SaveCodeArgsDto
        {
            TaskId = taskId,
            SourceCode = sourceCode
        };

        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");

        var response = await _client.PostAsJsonAsync("http://localhost:5000/solution/save", args);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Test]
    public async Task GetSavedCode_AfterSave()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        var args = new SaveCodeArgsDto
        {
            TaskId = taskId,
            SourceCode = sourceCode
        };

        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");

        var response = await _client.PostAsJsonAsync("http://localhost:5000/solution/save", args);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}