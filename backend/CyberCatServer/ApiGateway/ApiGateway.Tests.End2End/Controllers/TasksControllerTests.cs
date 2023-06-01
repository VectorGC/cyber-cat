using System.Threading.Tasks;
using ApiGateway.Tests.End2End.Extensions;
using Shared.Dto;

namespace ApiGateway.Tests.End2End.Controllers;

[TestFixture]
public class TasksControllerTests : E2ETests
{
    [Test]
    public async Task GetTask()
    {
        var taskId = "tutorial";

        var task = await Client.GetFromJsonAsync<TaskDto>($"/tasks/{taskId}");

        Assert.AreEqual("Hello cat!", task.Name);
        Assert.IsNotEmpty(task.Description);
    }
}