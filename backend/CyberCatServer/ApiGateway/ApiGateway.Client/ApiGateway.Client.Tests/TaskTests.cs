using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class TaskTests : AuthorizedClientTestFixture
    {
        public TaskTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";
            var client = await GetClient();

            var task = await client.Tasks.GetTask(taskId);

            Assert.AreEqual("Hello cat!", task.Name);
            Assert.IsNotEmpty(task.Description);
        }
    }
}