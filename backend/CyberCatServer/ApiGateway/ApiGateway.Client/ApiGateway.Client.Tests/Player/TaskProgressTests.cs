using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class TaskProgressTests
    {
        private readonly ServerEnvironment _serverEnvironment;

        public TaskProgressTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task GetSavedCode_AfterSaveAndDeleteCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
            var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];

                // We are checking that there is no code initially.
                Assert.IsFalse(task.IsStarted);

                // We are saving the code.
                await task.SubmitSolution(sourceCode);

                Assert.IsTrue(task.IsStarted);
                Assert.AreEqual(sourceCode, task.LastSolution);

                // We are modifying the saved code.
                await task.SubmitSolution(sourceCodeToChange);

                Assert.IsTrue(task.IsStarted);
                Assert.IsTrue(task.IsComplete);
                Assert.AreEqual(sourceCodeToChange, task.LastSolution);
            }
        }

        [Test]
        public async Task ChangeTaskStatus_WhenSubmitSolution()
        {
            var taskId = "tutorial";
            var errorCode = "#include <stdio.h>\nint main() { printf(\"Unexpected output!\"); }";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];

                Assert.IsFalse(task.IsStarted);

                var result = await task.SubmitSolution(errorCode);
                Assert.IsTrue(result.Value.IsFailure);

                Assert.IsTrue(task.IsStarted);
                Assert.AreEqual(errorCode, task.LastSolution);

                result = await task.SubmitSolution(completeCode);
                Assert.IsTrue(result.Value.IsSuccess);
                Assert.IsTrue(task.IsStarted);
                Assert.IsTrue(task.IsComplete);
                Assert.AreEqual(completeCode, task.LastSolution);
            }
        }

        [Test]
        public async Task CompleteTaskStatus_WhenPassCompleteSolutionOnStart()
        {
            var taskId = "tutorial";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];

                Assert.IsFalse(task.IsStarted);

                var result = await task.SubmitSolution(completeCode);
                Assert.IsTrue(result.Value.IsSuccess);
                Assert.IsTrue(task.IsStarted);
                Assert.IsTrue(task.IsComplete);
                Assert.AreEqual(completeCode, task.LastSolution);
            }
        }

        [Test]
        public async Task SaveUser_WhenUserCompleteTask()
        {
            var taskId = "tutorial";

            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks[taskId];

                var users = task.UsersWhoSolvedTask;
                Assert.IsEmpty(users);

                var verdict = await task.SubmitSolution(CodeSolution.Tutorial());
                Assert.IsTrue(verdict.Value.IsSuccess);

                Assert.AreEqual(1, users.Count);

                var user = users.First();
                Assert.AreEqual(client.Client.Player.User.FirstName, user.FirstName);
            }
        }
    }
}