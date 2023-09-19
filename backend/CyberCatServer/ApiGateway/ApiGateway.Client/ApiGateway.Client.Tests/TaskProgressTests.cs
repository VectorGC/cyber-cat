using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Internal.Tasks.Verdicts;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

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
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            var player = await GetPlayerClient();
            var task = player.Tasks[taskId];

            Assert.IsAssignableFrom<NotStarted>(await task.GetStatus());

            var verdict = await task.VerifySolution(errorCode);
            Assert.IsAssignableFrom<Failure>(verdict);

            Assert.IsAssignableFrom<HaveSolution>(await task.GetStatus());

            verdict = await task.VerifySolution(completeCode);
            Assert.IsAssignableFrom<Success>(verdict);

            Assert.IsAssignableFrom<Complete>(await task.GetStatus());
        }

        [Test]
        public async Task CompleteTaskStatus_WhenPassCompleteSolutionOnStart()
        {
            var taskId = "tutorial";
            var completeCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            var player = await GetPlayerClient();
            var task = player.Tasks[taskId];

            Assert.IsAssignableFrom<NotStarted>(await task.GetStatus());

            var verdict = await task.VerifySolution(completeCode);
            Assert.IsAssignableFrom<Success>(verdict);

            Assert.IsAssignableFrom<Complete>(await task.GetStatus());
        }
    }
}