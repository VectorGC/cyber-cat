using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class SolutionClientTests : AuthorizedClientTestFixture
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

            var client = await GetClient();

            // We are checking that there is no code initially.
            var lastSavedCode = await client.SolutionService.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);

            // We are saving the code.
            await client.SolutionService.SaveCode(taskId, sourceCode);

            lastSavedCode = await client.SolutionService.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCode, lastSavedCode);

            // We are modifying the saved code.
            await client.SolutionService.SaveCode(taskId, sourceCodeToChange);

            lastSavedCode = await client.SolutionService.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCodeToChange, lastSavedCode);

            // We are deleting and verifying that everything has been cleared.
            await client.SolutionService.RemoveSavedCode(taskId);

            lastSavedCode = await client.SolutionService.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);
        }
    }
}