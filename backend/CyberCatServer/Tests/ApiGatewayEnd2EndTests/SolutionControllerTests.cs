using System.Net.Http.Json;
using ApiGatewayEnd2EndTests.Extensions;

namespace ApiGatewayEnd2EndTests;

[TestFixture]
public class SolutionControllerTests
{
    private HttpClient _client;

    [SetUp]
    public async Task SetUp()
    {
        _client = new HttpClient();
        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");
    }

    [Test]
    public async Task GetSavedCode_AfterSaveAndDeleteCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
        var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        // Проверяем что изначально кода нет.
        var lastSavedCode = await _client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsEmpty(lastSavedCode);

        // Сохраняем код.
        var response = await _client.PostAsJsonAsync($"http://localhost:5000/solution/{taskId}", sourceCode);
        response.EnsureSuccessStatusCode();

        lastSavedCode = await _client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsNotEmpty(lastSavedCode);
        Assert.AreEqual(sourceCode, lastSavedCode);

        // Меняем сохраненный код.
        response = await _client.PostAsJsonAsync($"http://localhost:5000/solution/{taskId}", sourceCodeToChange);
        response.EnsureSuccessStatusCode();

        lastSavedCode = await _client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsNotEmpty(lastSavedCode);
        Assert.AreEqual(sourceCodeToChange, lastSavedCode);

        // Удаляем и проверяем, что все очистилось.
        await _client.DeleteAsync($"http://localhost:5000/solution/{taskId}");

        lastSavedCode = await _client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsEmpty(lastSavedCode);
    }
}