using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class TaskTests : PlayerClientTestFixture
    {
        public TaskTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";
            var client = await GetPlayerClient();

            var task = await client.Tasks.GetTask(taskId);

            Assert.AreEqual("Hello cat!", task.Description.Name);
            Assert.IsNotEmpty((string) task.Description.Description);
        }
    }
}