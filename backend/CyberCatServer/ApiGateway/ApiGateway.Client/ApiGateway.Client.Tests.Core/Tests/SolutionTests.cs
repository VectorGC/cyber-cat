using System.Threading.Tasks;
using ApiGateway.Client.Tests.Core.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Core.Tests
{
    [TestFixture]
    public abstract class SolutionTests : ClientTests
    {
        [Test]
        public async Task GetSavedCode_AfterSaveAndDeleteCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello world!\"); }";
            var sourceCodeToChange = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            // Проверяем что изначально кода нет.
            var lastSavedCode = await Client.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);

            // Сохраняем код.
            await Client.SaveCode(taskId, sourceCode);

            lastSavedCode = await Client.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCode, lastSavedCode);

            // Меняем сохраненный код.
            await Client.SaveCode(taskId, sourceCodeToChange);

            lastSavedCode = await Client.GetSavedCode(taskId);
            Assert.IsNotEmpty(lastSavedCode);
            Assert.AreEqual(sourceCodeToChange, lastSavedCode);

            // Удаляем и проверяем, что все очистилось.
            await Client.RemoveSavedCode(taskId);

            lastSavedCode = await Client.GetSavedCode(taskId);
            Assert.IsEmpty(lastSavedCode);
        }
    }
}