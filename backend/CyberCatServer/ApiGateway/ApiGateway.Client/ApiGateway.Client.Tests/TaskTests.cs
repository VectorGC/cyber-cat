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

            var task = client.Tasks[taskId];

            Assert.AreEqual("Hello cat!", await task.GetName());
            
            var description = await task.GetDescription();
            StringAssert.StartsWith("# Hello cat!", description);
        }
    }
}