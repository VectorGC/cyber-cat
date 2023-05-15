using System.Net.Http.Json;
using ApiGateway.Dto;
using ApiGatewayEnd2EndTests.Extensions;

namespace ApiGatewayEnd2EndTests;

public class TasksControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        _client = new HttpClient();
    }

    [Test]
    public async Task GetTask()
    {
        var taskId = "tutorial";
        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");

        var task = await _client.GetFromJsonAsync<TaskDto>($"http://localhost:5000/tasks/{taskId}");

        Assert.AreEqual("Hello world!", task.Name);
        Assert.IsNotEmpty(task.Description);
    }
}