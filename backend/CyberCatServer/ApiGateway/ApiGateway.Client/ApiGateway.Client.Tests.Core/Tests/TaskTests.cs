using System.Threading.Tasks;
using ApiGateway.Client.Tests.Core.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Core.Tests
{
    [TestFixture]
    public abstract class TaskTests : ClientTests
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
}