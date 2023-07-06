using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";
            var client = await TestClient.TestClient.Authorized();

            var task = await client.Tasks.GetTask(taskId);

            Assert.AreEqual("Hello cat!", task.Name);
            Assert.IsNotEmpty(task.Description);
        }
    }
}