using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class SolutionClientTests : PlayerClientTestFixture
    {
        public SolutionClientTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task GetSavedCode_AfterSaveAndDeleteCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
            var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            var player = await GetPlayerClient();
            var task = player.Tasks[taskId];

            // We are checking that there is no code initially.
            Assert.IsAssignableFrom<NotStarted>(await task.GetStatus());

            // We are saving the code.
            await task.VerifySolution(sourceCode);

            var status = await task.GetStatus() as HaveSolution;
            Assert.AreEqual(sourceCode, status.Solution);

            // We are modifying the saved code.
            await task.VerifySolution(sourceCodeToChange);

            var complete = await task.GetStatus() as Complete;
            Assert.AreEqual(sourceCodeToChange, complete.Solution);
        }
    }
}