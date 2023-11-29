using System.Threading.Tasks;
using ApiGateway.Client.Tests.V2.Extensions;
using ApiGateway.Client.V3.Application;
using ApiGateway.Client.V3.Domain;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.V2
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class TaskTestsV2 : ApiGatewayClientTestFixture
    {
        public TaskTestsV2(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";
            TaskModel task;

            using (var client = await GetTestPlayerClient())
            {
                task = client.Player.Tasks[taskId];
            }

            Assert.AreEqual("Hello cat!", task.Description.Name);
            StringAssert.StartsWith("# Hello cat!", task.Description.Description);
            Assert.AreEqual("#include <iostream>\r\n#include <stdio.h>\r\n\r\nint main()\r\n{\r\n    printf(\"Hello world!\");\r\n}\r\n", task.Description.DefaultCode);
        }
    }
}