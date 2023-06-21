using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";
            var client = await PlayerClient.Create();

            var task = await client.GetTask(taskId);

            Assert.AreEqual("Hello cat!", task.Name);
            Assert.IsNotEmpty(task.Description);
        }
    }
}