using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class TestCasesTests : ApiGatewayClientTestFixture
    {
        private IApiGatewayClient _client;

        public TestCasesTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _client = TestPlayerClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public void GetTutorialTestCase()
        {
            var taskId = "tutorial";

            var task = _client.Player.Tasks[taskId];
            var testCases = task.Tests;

            Assert.AreEqual(1, testCases.Count);
            Assert.IsNull(testCases[0].Inputs);
            Assert.AreEqual("Hello cat!", testCases[0].Expected);
        }

        [Test]
        public void GetTask1TestCase_WithInputParameters()
        {
            var taskId = "task-1";

            var task = _client.Player.Tasks[taskId];
            var testCases = task.Tests;

            Assert.AreEqual(3, testCases.Count);
            CollectionAssert.AreEqual(testCases[0].Inputs, new[] {"1", "1"});
            CollectionAssert.AreEqual(testCases[1].Inputs, new[] {"5", "10"});
            CollectionAssert.AreEqual(testCases[2].Inputs, new[] {"-1000", "1000"});
        }
    }
}