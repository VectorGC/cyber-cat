using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class PlayerTaskProgressTests
    {
        private readonly ServerEnvironment _serverEnvironment;

        public PlayerTaskProgressTests(ServerEnvironment serverEnvironment)
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
        public async Task CompleteTaskStatus_WhenPassCompleteSolution()
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

        [Test]
        public async Task FetchVerdictHistory_ForPlayer_OnLogin()
        {
            var taskId = "tutorial";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
            var email = "test@test.com";
            var password = "test_password";
            var userName = "Test_Name";

            using (var client = await TestPlayerClient.Create(_serverEnvironment, email, password, userName))
            {
                var task = client.Client.Player.Tasks[taskId];

                Assert.IsFalse(task.IsStarted);

                var result = await task.SubmitSolution(completeCode);
                Assert.IsTrue(result.Value.IsSuccess);
                Assert.IsTrue(task.IsStarted);
                Assert.IsTrue(task.IsComplete);
                Assert.AreEqual(completeCode, task.LastSolution);

                await client.Client.Player.Logout();
                var verdict = client.Client.VerdictHistoryService.GetLastVerdict(taskId);
                Assert.IsNull(verdict);

                await client.Client.LoginPlayer(email, password);
                // TODO: Restore verdict history.
                /*
                var verdictAfterLogin = client.Client.VerdictHistoryService.GetLastVerdict(taskId);
                Assert.IsNotNull(verdictAfterLogin);
                Assert.AreEqual(verdict.TaskId, verdictAfterLogin.TaskId);
                Assert.AreEqual(verdict.Solution, verdictAfterLogin.Solution);
                Assert.AreEqual(verdict.IsFailure, verdictAfterLogin.IsFailure);
                Assert.AreEqual(verdict.IsSuccess, verdictAfterLogin.IsSuccess);
                */
            }
        }

        [Test]
        public async Task SaveVerdictHistory_FromAnonymous_AfterLogin()
        {
            var email = "test@test.com";
            var password = "test_password";
            var userName = "Test_Name";
            var taskId = "tutorial";

            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                var result = await client.SubmitAnonymousSolution(taskId, CodeSolution.Tutorial());
                Assert.IsTrue(result.Value.IsSuccess);

                var register = await client.RegisterPlayer(email, password, userName);
                Assert.IsTrue(register.IsSuccess);

                var login = await client.LoginPlayer(email, password);
                Assert.IsTrue(login.IsSuccess);

                Assert.IsTrue(client.Player.Tasks[taskId].IsComplete);
                var verdict = client.VerdictHistoryService.GetBestOrLastVerdict(taskId);
                Assert.AreEqual(result.Value.TaskId, verdict.TaskId);
                Assert.AreEqual(result.Value.Solution, verdict.Solution);
                Assert.AreEqual(result.Value.IsFailure, verdict.IsFailure);
                Assert.AreEqual(result.Value.IsSuccess, verdict.IsSuccess);

                var logout = await client.Player.Logout();
                Assert.IsTrue(logout.IsSuccess);

                verdict = client.VerdictHistoryService.GetLastVerdict(taskId);
                Assert.IsNull(verdict);

                await client.LoginPlayer(email, password);
                // TODO: Restore verdict history.
                /*
                var verdictAfterLogin = client.VerdictHistoryService.GetLastVerdict(taskId);
                Assert.IsNotNull(verdictAfterLogin);
                Assert.AreEqual(result.Value.TaskId, verdictAfterLogin.TaskId);
                Assert.AreEqual(result.Value.Solution, verdictAfterLogin.Solution);
                Assert.AreEqual(result.Value.IsFailure, verdictAfterLogin.IsFailure);
                Assert.AreEqual(result.Value.IsSuccess, verdictAfterLogin.IsSuccess);

                var remove = await client.Player.Remove();
                Assert.IsTrue(remove.IsSuccess);
                */

                await client.Player.Remove();
            }
        }
    }
}