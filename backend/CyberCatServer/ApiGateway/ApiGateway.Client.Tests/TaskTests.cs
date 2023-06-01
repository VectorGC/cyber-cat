using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;

namespace ApiGateway.Client.Tests;

[TestFixture]
public class TaskTests : ClientTests
{
    [Test]
    public async Task GetTask()
    {
        var taskId = "tutorial";

        var task = await Client.GetTask(taskId);

        Assert.AreEqual("Hello cat!", task.Name);
        Assert.IsNotEmpty(task.Description);
    }
}