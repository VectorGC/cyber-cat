using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
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
            _client = TestPlayerClient();
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
            Assert.AreEqual(TestCodeSolution.PrintMessage("Hello world!"), task.Description.DefaultCode);
        }

        [Test]
        public async Task SaveUser_WhenUserCompleteTask()
        {
            var taskId = "tutorial";

            var task = _client.Player.Tasks[taskId];

            var users = task.UsersWhoSolvedTask;
            Assert.IsEmpty(users);

            var verdict = await task.SubmitSolution(TestCodeSolution.SolveTutorial());
            Assert.IsTrue(verdict.Value.IsSuccess);

            Assert.AreEqual(1, users.Count);

            var user = users.First();
            Assert.AreEqual(_client.Player.User.FirstName, user.FirstName);
        }
    }
}