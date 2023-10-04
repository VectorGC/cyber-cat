using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class TestCasesTests : PlayerClientTestFixture
    {
        public TestCasesTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task GetTutorialTestCase()
        {
            var taskId = "tutorial";
            var client = await GetPlayerClient();

            var task = client.Tasks[taskId];
            var testCases = await task.GetTestCases();

            Assert.AreEqual(1, testCases.Values.Count);
            Assert.IsNull(testCases[taskId, 0].Inputs);
            Assert.AreEqual("Hello cat!", testCases[taskId, 0].Expected);
        }

        [Test]
        public async Task GetTask1TestCase_WithInputParameters()
        {
            var taskId = "task-1";
            var client = await GetPlayerClient();

            var task = client.Tasks[taskId];
            var testCases = await task.GetTestCases();

            Assert.AreEqual(3, testCases.Values.Count);
            CollectionAssert.AreEqual(testCases[taskId, 0].Inputs, new[] {"1", "1"});
            CollectionAssert.AreEqual(testCases[taskId, 1].Inputs, new[] {"5", "10"});
            CollectionAssert.AreEqual(testCases[taskId, 2].Inputs, new[] {"-1000", "1000"});
        }
    }
}