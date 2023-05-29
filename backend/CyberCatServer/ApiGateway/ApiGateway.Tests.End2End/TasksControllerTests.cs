using System.Net.Http.Json;
using ApiGateway.Tests.End2End.Extensions;
using Shared.Dto;

namespace ApiGateway.Tests.End2End;

[TestFixture]
public class TasksControllerTests : E2ETests
{
    [Test]
    public async Task GetTask()
    {
        var taskId = "tutorial";

        var task = await Client.GetFromJsonAsync<TaskDto>($"http://localhost:5000/tasks/{taskId}");

        Assert.AreEqual("Hello cat!", task.Name);
        Assert.IsNotEmpty(task.Description);
    }
}