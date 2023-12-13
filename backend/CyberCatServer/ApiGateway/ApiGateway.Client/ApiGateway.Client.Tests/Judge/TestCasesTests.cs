using System.Threading.Tasks;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Judge
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class TestCasesTests
    {
        private readonly ServerEnvironment _serverEnvironment;

        public TestCasesTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task GetTutorialTestCase()
        {
            var taskId = "tutorial";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];
                var testCases = task.Tests;

                Assert.AreEqual(1, testCases.Count);
                Assert.IsNull(testCases[0].Inputs);
                Assert.AreEqual("Hello cat!", testCases[0].Expected);
            }
        }

        [Test]
        public async Task GetTask1TestCase_WithInputParameters()
        {
            var taskId = "task-1";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];
                var testCases = task.Tests;

                Assert.AreEqual(3, testCases.Count);
                CollectionAssert.AreEqual(testCases[0].Inputs, new[] {"1", "1"});
                CollectionAssert.AreEqual(testCases[1].Inputs, new[] {"5", "10"});
                CollectionAssert.AreEqual(testCases[2].Inputs, new[] {"-1000", "1000"});
            }
        }
    }
}