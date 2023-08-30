using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;
using Shared.Models.Enums;

namespace ApiGateway.Client.Tests
{
    public class TaskProgressTests : PlayerClientTestFixture
    {
        public TaskProgressTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task ChangeTaskStatus_WhenVerifySolution()
        {
            var taskId = "tutorial";
            var errorCode = "#include <stdio.h>\nint main() { printf(\"Unexpected output!\"); }";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";

            var client = await GetPlayerClient();

            var tutorialTask = await client.Tasks.GetTask(taskId);
            Assert.AreEqual(TaskProgressStatus.NotStarted, tutorialTask.Data.Status);

            var verdict = await client.PlayerService.VerifySolution(taskId, errorCode);
            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);

            tutorialTask = await client.Tasks.GetTask(taskId);
            Assert.AreEqual(TaskProgressStatus.HaveSolutions, tutorialTask.Data.Status);

            verdict = await client.PlayerService.VerifySolution(taskId, completeCode);
            Assert.AreEqual(VerdictStatus.Success, verdict.Status);

            tutorialTask = await client.Tasks.GetTask(taskId);
            Assert.AreEqual(TaskProgressStatus.Complete, tutorialTask.Data.Status);
        }

        [Test]
        public async Task CompleteTaskStatus_WhenPassCompleteSolutionOnStart()
        {
            var taskId = "tutorial";
            var errorCode = "#include <stdio.h>\nint main() { printf(\"Unexpected output!\"); }";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";

            var client = await GetPlayerClient();

            var tutorialTask = await client.Tasks.GetTask(taskId);
            Assert.AreEqual(TaskProgressStatus.NotStarted, tutorialTask.Data.Status);

            var verdict = await client.PlayerService.VerifySolution(taskId, completeCode);
            Assert.Equals(VerdictStatus.Success, verdict.Status);

            tutorialTask = await client.Tasks.GetTask(taskId);
            Assert.AreEqual(TaskProgressStatus.Complete, tutorialTask.Data.Status);
        }
    }
}