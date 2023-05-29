using System.Net.Http.Json;
using ApiGateway.Tests.End2End.Extensions;
using Shared.Dto;

namespace ApiGateway.Tests.End2End;

public class TasksControllerTests
{
    private HttpClient _client;

    [SetUp]
    public async Task SetUp()
    {
        _client = new HttpClient();
        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");
    }

    [Test]
    public async Task GetTask()
    {
        var taskId = "tutorial";

        var task = await _client.GetFromJsonAsync<TaskDto>($"http://localhost:5000/tasks/{taskId}");

        Assert.AreEqual("Hello cat!", task.Name);
        Assert.IsNotEmpty(task.Description);
    }
}