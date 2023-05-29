using System.Net.Http.Json;
using ApiGateway.Tests.End2End.Extensions;

namespace ApiGateway.Tests.End2End;

[TestFixture]
public class SolutionControllerTests : E2ETests
{
    [Test]
    public async Task GetSavedCode_AfterSaveAndDeleteCode()
    {
        var taskId = "tutorial";
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
        var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        // Проверяем что изначально кода нет.
        var lastSavedCode = await Client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsEmpty(lastSavedCode);

        // Сохраняем код.
        var response = await Client.PostAsJsonAsync($"http://localhost:5000/solution/{taskId}", sourceCode);
        response.EnsureSuccessStatusCode();

        lastSavedCode = await Client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsNotEmpty(lastSavedCode);
        Assert.AreEqual(sourceCode, lastSavedCode);

        // Меняем сохраненный код.
        response = await Client.PostAsJsonAsync($"http://localhost:5000/solution/{taskId}", sourceCodeToChange);
        response.EnsureSuccessStatusCode();

        lastSavedCode = await Client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsNotEmpty(lastSavedCode);
        Assert.AreEqual(sourceCodeToChange, lastSavedCode);

        // Удаляем и проверяем, что все очистилось.
        await Client.DeleteAsync($"http://localhost:5000/solution/{taskId}");

        lastSavedCode = await Client.GetStringAsync($"http://localhost:5000/solution/{taskId}");
        Assert.IsEmpty(lastSavedCode);
    }
}