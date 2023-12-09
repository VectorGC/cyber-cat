using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class TaskTests
    {
        private readonly ServerEnvironment _serverEnvironment;
        private ApiGatewayClient _client;

        public TaskTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }
        
        [SetUp]
        public void SetUp()
        {
            _client = new ApiGatewayClient(_serverEnvironment);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task GetTask()
        {
            var taskId = "tutorial";

            var result = await _client.TaskService.GetTaskDescriptions();
            var tasks = result.Value;
            var task = tasks[taskId];

            Assert.AreEqual("Hello cat!", task.Name);
            StringAssert.StartsWith("# Hello cat!", task.Description);
            Assert.AreEqual(CodeSolution.PrintMessage("Hello world!"), task.DefaultCode);
        }
    }
}