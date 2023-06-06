using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class SolutionTests
    {
        [Test]
        public async Task GetSavedCode_AfterSaveAndDeleteCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
            var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
            var client = await PlayerClient.Create();

            // Проверяем что изначально кода нет.
            var lastSavedCode = await client.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);

            // Сохраняем код.
            await client.SaveCode(taskId, sourceCode);

            lastSavedCode = await client.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCode, lastSavedCode);

            // Меняем сохраненный код.
            await client.SaveCode(taskId, sourceCodeToChange);

            lastSavedCode = await client.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCodeToChange, lastSavedCode);

            // Удаляем и проверяем, что все очистилось.
            await client.RemoveSavedCode(taskId);

            lastSavedCode = await client.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);
        }
    }
}