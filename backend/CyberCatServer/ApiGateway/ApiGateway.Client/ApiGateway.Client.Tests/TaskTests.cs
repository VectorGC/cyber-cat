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
            Assert.IsNotEmpty(await task.GetDescription());
            Assert.AreEqual("#include <iostream>\n\nint main() {\n\t//Твой код здесь\n\n\treturn 0;\n}", await task.GetDefaultCode());
        }
    }
}