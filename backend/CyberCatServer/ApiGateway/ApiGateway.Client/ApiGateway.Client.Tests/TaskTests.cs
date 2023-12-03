using ApiGateway.Client.Tests.Extensions;
using ApiGateway.Client.V3.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class TaskTests : ApiGatewayClientTestFixture
    {
        private IApiGatewayClient _client;

        public TaskTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _client = GetTestPlayerClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public void GetTask()
        {
            var taskId = "tutorial";

            var task = _client.Player.Tasks[taskId];

            Assert.AreEqual("Hello cat!", task.Description.Name);
            StringAssert.StartsWith("# Hello cat!", task.Description.Description);
            Assert.AreEqual("#include <iostream>\r\n#include <stdio.h>\r\n\r\nint main()\r\n{\r\n    printf(\"Hello world!\");\r\n}\r\n", task.Description.DefaultCode);
        }
    }
}